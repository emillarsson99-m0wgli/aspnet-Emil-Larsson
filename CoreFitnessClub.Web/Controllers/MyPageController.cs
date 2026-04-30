using CoreFitnessClub.Application.Interfaces;
using CoreFitnessClub.Infrastructure.Identity;
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
    
}
