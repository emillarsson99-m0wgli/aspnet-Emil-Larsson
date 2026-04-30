using CoreFitnessClub.Application.Interfaces;
using CoreFitnessClub.Infrastructure.Identity;
using CoreFitnessClub.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoreFitnessClub.Web.Controllers;

public class MyPageController : Controller
{
    private readonly IBookingService _bookingService;
    private readonly IMembershipService _membershipService;
    private readonly UserManager<ApplicationUser> _userManager;

    public MyPageController(IBookingService bookingService, IMembershipService membershipService, UserManager<ApplicationUser> userManager)
    {
        _bookingService = bookingService;
        _membershipService = membershipService;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);

        if (user == null)
            return Challenge();

        var bookings = await _bookingService.GetBookingsByUserIdAsync(user.Id);
        var membership = await _membershipService.GetByUserIdAsync(user.Id);

        var model = new MyPageViewModel
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email ?? string.Empty,
            PhoneNumber = user.PhoneNumber ?? string.Empty,
            Bookings = bookings,
            Membership = membership
        };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateProfile(MyPageViewModel model)
    {
        if (!ModelState.IsValid)
            return View("Index", model);

        var user = await _userManager.GetUserAsync(User);

        if (user == null)
            return Challenge();

        user.FirstName = model.FirstName;
        user.LastName = model.LastName;
        user.PhoneNumber = model.PhoneNumber;

        if (user.Email != model.Email)
        {
            user.Email = model.Email;
            user.UserName = model.Email;
            user.NormalizedEmail = model.Email.ToUpper();
            user.NormalizedUserName = model.Email.ToUpper();
            user.EmailConfirmed = false;
        }

        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View("Index", model);
        }

        TempData["SuccessMessage"] = "Your profile has been updated";
        return RedirectToAction(nameof(Index));
    }
}
