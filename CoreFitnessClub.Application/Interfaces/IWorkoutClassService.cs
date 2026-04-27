using CoreFitnessClub.Domain.Entities;

namespace CoreFitnessClub.Application.Interfaces;

public interface IWorkoutClassService
{
    Task<List<WorkoutClass>> GetAllAsync();
    Task<WorkoutClass?> GetByIdAsync(int id);
}
