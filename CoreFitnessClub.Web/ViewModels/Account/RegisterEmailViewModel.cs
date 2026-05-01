using System.ComponentModel.DataAnnotations;

namespace CoreFitnessClub.Web.ViewModels.Account;

public class RegisterEmailViewModel
{
    [Required] 
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
}
