using System.ComponentModel.DataAnnotations;

namespace CoreFitnessClub.Web.ViewModels.Account;

public class SetPasswordViewModel
{
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    [Required]
    [Compare("Password")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; } = string.Empty;
}
