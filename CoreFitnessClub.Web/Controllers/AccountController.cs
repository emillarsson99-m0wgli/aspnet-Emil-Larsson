using CoreFitnessClub.Infrastructure.Identity;
using CoreFitnessClub.Web.ViewModels;
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
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var user = new ApplicationUser
        {
            UserName = model.Email,
            Email = model.Email
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, isPersistent: false);
            return RedirectToAction("index", "Home");
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return View(model);

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

        var info = await _signInManager.GetExternalLoginInfoAsync();
        if (info is null)
        {
            logger.LogWarning("External login info is null.");
            return ExternalLoginFailed(returnUrl);
        }

        var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);

        if (result.Succeeded)
            return RedirectToLocal(returnUrl);

        var email = info.Principal.FindFirstValue(ClaimTypes.Email);

        if (string.IsNullOrWhiteSpace(email))
        {
            logger.LogWarning("No email claim from {provider}.", info.LoginProvider);
            return ExternalLoginFailed(returnUrl);
        }

        return await ExternalVerification(email, returnUrl);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("index", "Home");
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

    //[HttpPost, ValidateAntiForgeryToken]
    //public async Task<IActionResult> VerifyExternalLogin(VerifyExternalLoginViewModel vm)
    //{
    //    if (!ModelState.IsValid)
    //        return View("VerifyExternalLogin", vm);

    //    if (!string.Equals(vm.Code, "12345", StringComparison.Ordinal))
    //    {
    //        ModelState.AddModelError(nameof(vm.Code), "Invalid code.");
    //        return View("VerifyExternalLogin", vm);
    //    }

    //    var existingUser = await _userManager.FindByEmailAsync(email);
    //}
}
