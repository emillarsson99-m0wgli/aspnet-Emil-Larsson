using CoreFitnessClub.Application.Interfaces;
using CoreFitnessClub.Domain.Entities;
using CoreFitnessClub.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CoreFitnessClub.Infrastructure.Repos;

public class WorkoutClassRepo : IWorkoutClassRepo
{
    private readonly ApplicationDbContext _context;

    public WorkoutClassRepo(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<WorkoutClass>> GetAllAsync()
    {
        return await _context.WorkoutClasses
            .OrderBy(x => x.Date)
            .ThenBy(x => x.StartTime)
            .ThenBy(x => x.Name)
            .ToListAsync();
    }

    public async Task<WorkoutClass?> GetByIdAsync(int id)
    {
        return await _context.WorkoutClasses
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}
