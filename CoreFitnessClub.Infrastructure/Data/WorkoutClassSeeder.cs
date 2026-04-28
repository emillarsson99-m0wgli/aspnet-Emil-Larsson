using CoreFitnessClub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoreFitnessClub.Infrastructure.Data;

public static class WorkoutClassSeeder
{
    public static void SeedWorkoutClasses(ModelBuilder builder)
    {
        builder.Entity<WorkoutClass>().HasData(new WorkoutClass
        {
            Id = 1,
            Name = "Yoga",
            Category = "Mind & Body",
            Instructor = "Alice Smith",
            Date = new DateTime(2024, 7, 1),
            StartTime = new TimeSpan(17, 0, 0),
            EndTime = new TimeSpan(18, 0, 0),
            Capacity = 20
        },
        new WorkoutClass
        {
            Id = 2,
            Name = "Strength training",
            Category = "Strength",
            Instructor = "Phil Heath",
            Date = new DateTime(2024, 7, 1),
            StartTime = new TimeSpan(17, 0, 0),
            EndTime = new TimeSpan(19, 0, 0),
            Capacity = 10
        },
        new WorkoutClass
        {
            Id = 3,
            Name = "BJJ",
            Category = "Grappling",
            Instructor = "John Danaher",
            Date = new DateTime(2024, 7, 2),
            StartTime = new TimeSpan(17, 0, 0),
            EndTime = new TimeSpan(19, 0, 0),
            Capacity = 10
        },
        new WorkoutClass
        {
            Id = 4,
            Name = "Kickboxing",
            Category = "Striking",
            Instructor = "Alex Pereira",
            Date = new DateTime(2024, 7, 3),
            StartTime = new TimeSpan(17, 0, 0),
            EndTime = new TimeSpan(19, 0, 0),
            Capacity = 10
        });
    
    }
}

