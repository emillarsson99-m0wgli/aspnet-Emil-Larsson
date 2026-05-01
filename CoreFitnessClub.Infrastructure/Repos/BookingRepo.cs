using CoreFitnessClub.Application.Interfaces;
using CoreFitnessClub.Domain.Entities;
using CoreFitnessClub.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CoreFitnessClub.Infrastructure.Repos;

public class BookingRepo : IBookingRepo
{
    private readonly ApplicationDbContext _context;

    public BookingRepo(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Booking>> GetByUserIdAsync(string userId)
    {
        return await _context.Bookings
            .Include(x => x.WorkoutClass)
            .Where (x => x.UserId == userId)
            .OrderBy(x => x.WorkoutClass.Date)
            .ThenBy(x => x.WorkoutClass.StartTime)
            .ThenBy(x => x.WorkoutClass.Name)
            .ToListAsync();
    }

    public async Task<Booking?> GetByIdAsync(int id)
    {
        return await _context.Bookings
            .Include(x => x.WorkoutClass)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> ExistsAsync(string userId, int workoutClassId)
    {
        return await _context.Bookings
            .AnyAsync(x => x.UserId == userId && x.WorkoutClassId == workoutClassId);
    }

    public async Task AddAsync(Booking booking)
    {
        _context.Bookings.Add(booking);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Booking booking)
    {
        _context.Bookings.Remove(booking);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteByUserIdAsync(string userId)
    {
        var bookings = await _context.Bookings
            .Where(x => x.UserId == userId)
            .ToListAsync();

        _context.Bookings.RemoveRange(bookings);
        await _context.SaveChangesAsync();
    }    
}
