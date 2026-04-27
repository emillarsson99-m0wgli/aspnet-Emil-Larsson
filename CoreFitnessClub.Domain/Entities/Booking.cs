namespace CoreFitnessClub.Domain.Entities;

public class Booking
{
    public int Id { get; set; }
    public string UserId { get; set; } = null!;
    public int WorkoutClassId { get; set; }
    public DateTime BookedAt { get; set; } = DateTime.UtcNow;
    public WorkoutClass WorkoutClass { get; set; } = null!;
}
