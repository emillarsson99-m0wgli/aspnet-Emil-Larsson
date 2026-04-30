using CoreFitnessClub.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace CoreFitnessClub.Web.ViewModels;

public class MyPageViewModel
{
    [Display(Name = "First Name")]
    public string? FirstName { get; set; }

    [Display(Name = "Last Name")]
    public string? LastName { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Display(Name = "Phone Number")]
    public string? PhoneNumber { get; set; }
    public Membership? Membership { get; set; }
    public List<Booking> Bookings { get; set; } = new();
}
