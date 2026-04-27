using CoreFitnessClub.Domain.Entities;

namespace CoreFitnessClub.Application.Interfaces;

public interface IWorkoutClassService
{
    Task<List<WorkoutClass>> GetWorkoutClassesAsync();
    Task<WorkoutClass> GetWorkoutClassByIdAsync(int id);
}
