using System.ComponentModel.DataAnnotations;

namespace CoreFitnessClub.Web.ViewModels.Memberships;

public class CreateMembershipViewModel
{
    [Required(ErrorMessage = "Du måste välja medlemskapstyp.")]
    [Display(Name = "Medlemskapstyp")]
    public string Type { get; set; } = string.Empty;
    public MembershipDetailsViewModel? Membership { get; set; }
    public List<MembershipsOfferViewmodel> MembershipsOffers { get; set; } = new();
    public List<AccordionViewmodel> AccordionItems { get; set; } = new();
}
