using CoreFitnessClub.Domain.Entities;

namespace CoreFitnessClub.Application.Interfaces;

public interface IBookingRepo
{
    Task<List<Booking>> GetByUserIdAsync(string userId);
    Task<Booking?> GetByIdAsync(int id);
    Task<bool> ExistsAsync(string userId, int workoutClassId);
    Task AddAsync(Booking booking);
    Task DeleteAsync(Booking booking);
    Task DeleteByUserIdAsync(string userId);
}

