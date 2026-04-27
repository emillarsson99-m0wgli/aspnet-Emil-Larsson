using CoreFitnessClub.Domain.Entities;
using CoreFitnessClub.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CoreFitnessClub.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Membership> Membership { get; set; }
    public DbSet<WorkoutClass> WorkoutClasses { get; set; }
    public DbSet<Booking> Bookings { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Booking>()
            .HasOne(b => b.WorkoutClass)
            .WithMany(wc => wc.Bookings)
            .HasForeignKey(b => b.WorkoutClassId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Booking>()
            .HasIndex(b => new { b.UserId, b.WorkoutClassId })
            .IsUnique();
    }
}
