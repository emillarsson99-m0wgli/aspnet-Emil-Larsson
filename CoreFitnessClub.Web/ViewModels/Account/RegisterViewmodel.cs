using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CoreFitnessClub.Web.ViewModels.Account;

public class RegisterViewModel
{
    public string? ReturnUrl { get; set; }
    public List<string> ExternalProviders { get; set; } = [];

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public string Email { get; set; } = string.Empty;
}
