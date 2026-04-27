namespace CoreFitnessClub.Domain.Entities;

public class Membership
{
    public int Id { get; set; }
    public string UserId { get; set; } = null!;
    public string MembershipType { get; set; } = null!;
    public string Status { get; set; } = "Active";
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

}
