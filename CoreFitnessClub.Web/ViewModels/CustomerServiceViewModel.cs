using Microsoft.AspNetCore.Mvc;

namespace CoreFitnessClub.Web.ViewModels;

public class CustomerServiceViewModel
{
    public List<AccordionViewmodel> AccordionViews { get; set; } = new();
}
