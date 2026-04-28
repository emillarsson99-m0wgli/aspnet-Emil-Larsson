using CoreFitnessClub.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CoreFitnessClub.Web.Controllers;

public class WorkoutClassesController : Controller
{
    private readonly IWorkoutClassService _workoutClassService;

    public WorkoutClassesController(IWorkoutClassService workoutClassService)
    {
        _workoutClassService = workoutClassService;
    }

    public async Task<IActionResult> Index()
    {
        var classes = await _workoutClassService.GetAllAsync();
        return View(classes);
    }
}
