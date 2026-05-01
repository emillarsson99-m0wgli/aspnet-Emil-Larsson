using CoreFitnessClub.Infrastructure.Identity;
using CoreFitnessClub.Web.ViewModels;
using CoreFitnessClub.Web.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;
using System.Security.Claims;

namespace CoreFitnessClub.Web.Controllers;

public class AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<AccountController> logger ) : Controller
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager;

    [HttpGet]
    public async Task<IActionResult> Register(string? returnUrl = null)
    {
        var schemes = await _signInManager.GetExternalAuthenticationSchemesAsync();

        var vm = new RegisterViewModel
        {
            ReturnUrl = returnUrl,
            ExternalProviders = [.. schemes.Select(x => x.Name)]
        };
        return View(vm);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        TempData["Email"] = model.Email;
        TempData["ReturnUrl"] = model.ReturnUrl;

        return RedirectToAction("SetPassword");
    }

    [HttpGet]
    public IActionResult SetPassword()
    {
        var email = TempData["Email"]?.ToString();

        if (string.IsNullOrWhiteSpace(email))
            return RedirectToAction("Register");

        TempData.Keep("Email");
        ViewBag.Email = email;

        return View(new SetPasswordViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SetPassword(SetPasswordViewModel model)
    {
        var email = TempData["Email"]?.ToString();

        if (string.IsNullOrEmpty(email))
            return RedirectToAction("Register");

        if (!ModelState.IsValid)
        {
            TempData.Keep("Email");
            ViewBag.Email = email;
            return View(model);
        }

        var user = new ApplicationUser
        {
            UserName = email,
            Email = email,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            TempData.Keep("Email");
            ViewBag.Email = email;
            return View(model);
        }
        await _signInManager.SignInAsync(user, isPersistent: false);

        return RedirectToAction("index", "Home");
    }

    [HttpGet]
    public async Task<IActionResult> Login(string? returnUrl = null)
    {
        var schemes = await _signInManager.GetExternalAuthenticationSchemesAsync();

        var vm = new LoginViewModel
        {
            ReturnUrl = returnUrl,
            ExternalProviders = [.. schemes.Select(x => x.Name)]
        };
        return View(vm);
    }

    [HttpPost, ValidateAntiForgeryToken]

    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var result = await _signInManager.PasswordSignInAsync(
            model.Email,
            model.Password,
            model.RememberMe,
            lockoutOnFailure: false);

        if (result.Succeeded)
        {
            return RedirectToAction("index", "Home");
        }

        ModelState.AddModelError(string.Empty, "Wrong email or password!");
        return View(model);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult ExternalLogin(string provider, string? returnUrl = null)
    {
        if (string.IsNullOrWhiteSpace(provider))
            return RedirectToAction(nameof(Login), new { returnUrl });

        var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Account", new { returnUrl });
        var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);

        return Challenge(properties, provider);
    }

    [HttpGet]
    public async Task<IActionResult> ExternalLoginCallback(string? returnUrl = null, string? remoteError = null) //remoteError fångar upp eventuella fel från externa leverantörer(t ex om användaren avbryter inloggningen)
    {
        if (remoteError is not null)
        {
            logger.LogWarning("Remote error from provider: {Error}", remoteError);
            return ExternalLoginFailed(returnUrl);
        }

        var externalUser = await GetExternalUserInfo();
        if (externalUser is null)
            return ExternalLoginFailed(returnUrl);

        var (info, email) = externalUser.Value;

        var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);

        if (result.Succeeded)
            return RedirectToLocal(returnUrl);

        return await ExternalVerification(email, returnUrl);
    }


    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> VerifyExternalLogin(VerifyExternalLoginViewModel vm)
    {
        if (!ModelState.IsValid)
            return View("VerifyExternalLogin", vm);

        if (!string.Equals(vm.Code, "12345", StringComparison.Ordinal))
        {
            ModelState.AddModelError(nameof(vm.Code), "Invalid code.");
            return View("VerifyExternalLogin", vm);
        }

        var externalUser = await GetExternalUserInfo();
        if (externalUser is null)
            return ExternalLoginFailed(vm.ReturnUrl);

        var (info, email) = externalUser.Value;

        var existingUser = await _userManager.FindByEmailAsync(email);
        if (existingUser is not null) 
            return await LinkExistingUser(existingUser, info, vm.ReturnUrl);

        return await CreateExternalUser(email, info, vm.ReturnUrl);
    }

    private async Task<IActionResult> LinkExistingUser(ApplicationUser user, ExternalLoginInfo info, string? returnUrl = null)
    {
        var result = await _userManager.AddLoginAsync(user, info);
        if (!result.Succeeded)
        {
            logger.LogError("Failed to link {Provider} to {Email} : {Errors}",
                info.LoginProvider,
                user.Email,
                string.Join(",", result.Errors.Select(x => x.Description))
                );
            return ExternalLoginFailed(returnUrl);
        }
        await _signInManager.SignInAsync(user, isPersistent: false);
        return RedirectToLocal(returnUrl);
    }

    private async Task<IActionResult> CreateExternalUser(string email, ExternalLoginInfo info, string? returnUrl = null)
    {
        var user = new ApplicationUser
        {
            UserName = email,
            Email = email,
            EmailConfirmed = true
        };

        var createResult = await _userManager.CreateAsync(user);
        if (!createResult.Succeeded)
        {
            logger.LogError("Failed to create user {Email} : {Errors}",
                email,
                string.Join(",", createResult.Errors.Select(x => x.Description))
                );
            return ExternalLoginFailed(returnUrl);
        }

        var linkResult = await _userManager.AddLoginAsync(user, info);
        if (!linkResult.Succeeded)
        {
            logger.LogError("Failed to link {Provider} to {Email} : {Errors}",
                info.LoginProvider,
                user.Email,
                string.Join(",", linkResult.Errors.Select(x => x.Description))
                );
            return ExternalLoginFailed(returnUrl);
        }

        await _signInManager.SignInAsync(user, isPersistent: false);
        return RedirectToLocal(returnUrl);
    }
    

    private async Task<(ExternalLoginInfo info, string Email)?> GetExternalUserInfo()
    {
        var info = await _signInManager.GetExternalLoginInfoAsync();
        if (info is null)
        {
            logger.LogWarning("External login info is null.");
            return null;
        }

        var email = info.Principal.FindFirstValue(ClaimTypes.Email);
        if (string.IsNullOrWhiteSpace(email))
        {
            logger.LogWarning("No email claim from {provider}.", info.LoginProvider);
            return null;
        }
        return (info, email);
    }

    private RedirectToActionResult ExternalLoginFailed(string? returnUrl = null)
    {
        TempData["Error"] = "Log in failed, please try again.";
        return RedirectToAction(nameof(Login), new { returnUrl });
    }

    private IActionResult RedirectToLocal(string? returnUrl = null)
    {
        if (Url.IsLocalUrl(returnUrl))
            return Redirect(returnUrl);

        return RedirectToAction("index", "Home");
    }

    private async Task<IActionResult> ExternalVerification(string email, string? returnUrl = null)
    {
        return View("VerifyExternalLogin", new VerifyExternalLoginViewModel
        {
            ReturnUrl = returnUrl,
            Email = email,
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("index", "Home");
    }
}
