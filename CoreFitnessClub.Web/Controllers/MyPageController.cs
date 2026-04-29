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
        var userId = _userManager.GetUserId(User);

        if (userId == null)
            return Challenge();

        var bookings = await _bookingService.GetBookingsByUserIdAsync(userId);
        var membership = await _membershipService.GetByUserIdAsync(userId);

        ViewBag.Membership = membership;

        return View(bookings);
    }
    
}
