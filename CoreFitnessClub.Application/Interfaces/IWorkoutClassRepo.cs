using CoreFitnessClub.Domain.Entities;

namespace CoreFitnessClub.Application.Interfaces;

public interface IWorkoutClassRepo
{
    Task<List<WorkoutClass>> GetAllAsync();
    Task<WorkoutClass?> GetByIdAsync(int id);
}
