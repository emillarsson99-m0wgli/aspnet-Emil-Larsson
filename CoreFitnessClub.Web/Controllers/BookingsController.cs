using CoreFitnessClub.Application.Interfaces;
using CoreFitnessClub.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoreFitnessClub.Web.Controllers;

[Authorize]
public class BookingsController : Controller
{
    private readonly IBookingService _bookingService;
    private readonly UserManager<ApplicationUser> _userManager;

    public BookingsController(IBookingService bookingService, UserManager<ApplicationUser> userManager)
    {
        _bookingService = bookingService;
        _userManager = userManager;
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Book(int workoutClassId)
    {
        var userId = _userManager.GetUserId(User);

        if (userId == null)
            return Challenge();

        var success = await _bookingService.BookClassAsync(userId, workoutClassId);

        if (!success)
        {
            TempData["Error"] = "Unable to book the class. You may have already booked it or the class is full.";
            return RedirectToAction("Index", "WorkoutClasses");
        }

        TempData["SuccessMessage"] = "Class booked successfully!";
        return RedirectToAction("Index", "WorkoutClasses");
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Cancel(int bookingId)
    {
        var userId = _userManager.GetUserId(User);

        if (userId == null)
            return Challenge();

        var success = await _bookingService.CancelBookingAsync(userId, bookingId);

        if (!success)
        {
            TempData["ErrorMessage"] = "Unable to cancel the booking. It may have already been canceled or does not exist.";
            return RedirectToAction("MyBookings");
        }
        TempData["SuccessMessage"] = "Booking canceled successfully!";
        return RedirectToAction("MyBookings");
    }

    [HttpGet]
    public async Task<IActionResult> MyBookings()
    {
        var userId = _userManager.GetUserId(User);

        if (userId == null)
            return Challenge();

        var bookings = await _bookingService.GetBookingsByUserIdAsync(userId);
        return View(bookings);
    }
}
