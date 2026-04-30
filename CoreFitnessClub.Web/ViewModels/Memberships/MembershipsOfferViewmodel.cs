using Microsoft.AspNetCore.Mvc;

namespace CoreFitnessClub.Web.ViewModels.Memberships
{
    public class MembershipsOfferViewmodel
    {
        public string Type { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int MonthlyClasses { get; set; }
        public List<string> Benefits { get; set; } = new ();

    }
}
