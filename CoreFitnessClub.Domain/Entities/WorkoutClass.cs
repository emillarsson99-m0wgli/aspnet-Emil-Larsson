namespace CoreFitnessClub.Domain.Entities;

public class WorkoutClass
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime Date { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public string Instructor { get; set; } = null!;
    public string Category { get; set; } = null!;
    public int Capacity { get; set; }
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
