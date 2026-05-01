using Microsoft.AspNetCore.Mvc;

namespace CoreFitnessClub.Web.ViewModels;

public class CustomerServiceViewModel
{
    public List<AccordionViewmodel> AccordionItems { get; set; } = new();
}
