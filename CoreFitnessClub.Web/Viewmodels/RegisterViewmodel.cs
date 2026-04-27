using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CoreFitnessClub.Web.ViewModels;

public class RegisterViewModel
{
    public string? ReturnUrl { get; set; }
    public List<string> ExternalProviders { get; set; } = [];

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required.")]
    [DataType(DataType.Password)]
    [MinLength(8, ErrorMessage = "The password must be at least 8 characters long.")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Confirm your password.")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "The passwords must match.")]
    public string ConfirmPassword { get; set; } = string.Empty;
}
