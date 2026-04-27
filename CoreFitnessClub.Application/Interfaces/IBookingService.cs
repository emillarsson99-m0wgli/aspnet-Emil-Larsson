using CoreFitnessClub.Domain.Entities;

namespace CoreFitnessClub.Application.Interfaces;

public interface IBookingService
{
    Task<List<Booking>> GetBookingsByUserIdAsync (string userId);
    Task<bool> BookClassAsync(string userId, int workoutClassId);
    Task<bool> CancelBookingAsync (string userId, int workoutClassId);
}
