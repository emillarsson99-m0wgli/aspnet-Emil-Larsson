using Microsoft.AspNetCore.Identity;

namespace CoreFitnessClub.Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? ProfileImagePath { get; set; }
}
