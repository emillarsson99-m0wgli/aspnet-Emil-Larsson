using CoreFitnessClub.Application.Interfaces;
using CoreFitnessClub.Domain.Entities;

namespace CoreFitnessClub.Application.Services;

public class WorkoutClassService
{
    private readonly IWorkoutClassRepo _workoutClassRepo;

    public WorkoutClassService(IWorkoutClassRepo workoutClassRepo)
    {
        _workoutClassRepo = workoutClassRepo;
    }

    public async Task<List<WorkoutClass>> GetAllAsync()
    {
        return await _workoutClassRepo.GetAllAsync();
    }

    public async Task<WorkoutClass?> GetByIdAsync(int id)
    {
        return await _workoutClassRepo.GetByIdAsync(id);
    }
}
