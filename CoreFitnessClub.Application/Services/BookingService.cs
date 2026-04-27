using CoreFitnessClub.Application.Interfaces;
using CoreFitnessClub.Domain.Entities;

namespace CoreFitnessClub.Application.Services;

public class BookingService : IBookingService
{
    private readonly IBookingRepo _bookingRepo;
    private readonly IWorkoutClassRepo _workoutClassRepo;

    public BookingService(IBookingRepo bookingRepo, IWorkoutClassRepo workoutClassRepo)
    {
        _bookingRepo = bookingRepo;
        _workoutClassRepo = workoutClassRepo;
    }

    public async Task<List<Booking>> GetBookingsByUserIdAsync(string userId)
    {
        return await _bookingRepo.GetByUserIdAsync(userId);
    }

    public async Task<bool> BookClassAsync(string userId, int workoutClassId)
    {
        var workoutClass = await _workoutClassRepo.GetByIdAsync(workoutClassId);

        if (workoutClass == null)
            return false;

        var alreadyBooked = await _bookingRepo.ExistsAsync(userId, workoutClassId);

        if (alreadyBooked)
            return false;

        var booking = new Booking
        {
            UserId = userId,
            WorkoutClassId = workoutClassId,
            BookedAt = DateTime.UtcNow
        };

        await _bookingRepo.AddAsync(booking);

        return true;
    }

    public async Task<bool> CancelBookingAsync(string userId, int bookingId)
    {
        var booking = await _bookingRepo.GetByIdAsync(bookingId);

        if (booking == null)
            return false;

        if (booking.UserId != userId)
            return false;

        await _bookingRepo.DeleteAsync(booking);

        return true;
    }
}
