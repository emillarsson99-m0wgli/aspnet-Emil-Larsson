using Microsoft.AspNetCore.Mvc;

namespace CoreFitnessClub.Web.ViewModels
{
    public class AccordionViewmodel
    {
        public int Id { get; set; }
        public string Question { get; set; } = string.Empty;
        public List<string> Answers { get; set; } = new ();
    }
}
