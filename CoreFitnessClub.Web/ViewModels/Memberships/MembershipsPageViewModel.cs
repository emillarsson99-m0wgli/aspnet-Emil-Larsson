using Microsoft.AspNetCore.Mvc;

namespace CoreFitnessClub.Web.ViewModels.Memberships;

public class MembershipsPageViewModel
{
    public MembershipDetailsViewModel? Membership { get; set; }
    public CreateMembershipViewModel? CreateMembership { get; set; }
    public List<MembershipsOfferViewmodel> MembershipsOffers { get; set; } = new();
    public List<AccordionViewmodel> AccordionItems { get; set; } = new();
}
