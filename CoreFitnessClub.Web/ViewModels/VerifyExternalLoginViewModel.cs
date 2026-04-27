using System.ComponentModel.DataAnnotations;

namespace CoreFitnessClub.Web.ViewModels;

public class VerifyExternalLoginViewModel
{
    public string Email { get; set; } = null!;
    public string? ReturnUrl { get; set; }

    [Required(ErrorMessage = "Code is required.")]
    public string Code { get; set; } = null!;
}
