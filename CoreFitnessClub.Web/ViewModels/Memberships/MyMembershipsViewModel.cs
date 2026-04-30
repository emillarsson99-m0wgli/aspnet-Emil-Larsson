using Microsoft.AspNetCore.Mvc;

namespace CoreFitnessClub.Web.ViewModels.Memberships
{
    public class MyMembershipsViewmodel
    {
        public MembershipDetailsViewModel? Membership { get; set; }
        public List<AccordionViewmodel> AccordionItems { get; set; } = new();
    }
}
