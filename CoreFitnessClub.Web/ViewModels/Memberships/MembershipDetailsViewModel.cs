using Microsoft.AspNetCore.Mvc;

namespace CoreFitnessClub.Web.ViewModels.Memberships;

public class MembershipDetailsViewModel
{
    public string MembershipType { get; set; } = null!;
    public string Status { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
