using CoreFitnessClub.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace CoreFitnessClub.Web.Controllers;

public class CustomerServiceController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        var model = new CustomerServiceViewModel
        {
            AccordionItems = new List<AccordionViewmodel>
            {
                new()
                {
                    Id = 1,
                    Question = "Do I need prior gym experience to join CoreFitness?",
                    Answers =
                    {
                        "No, you don’t need any prior gym experience. Our gym is suitable for complete beginners as well as experienced members.",
                        "Our trainers can help you get started, show you how to use the equipment, and guide you toward your goals."
                    }
                },
                new()
                {
                    Id = 2,
                    Question = "What facilities are included with the membership?",
                    Answers =
                    {
                        "Your membership includes access to cardio machines, strength equipment, free weights, locker rooms, and showers.",
                        "Depending on your plan, you may also get access to group classes and selected training programs."
                    }
                },
                new()
                {
                    Id = 3,
                    Question = "Can I try the gym before taking a membership?",
                    Answers =
                    {
                        "Yes, you can try CoreFitness before choosing a membership. We offer a trial option so you can experience the gym first.",
                        "You can also speak with our staff if you want a guided tour or help choosing the right plan."
                    }
                },
                new()
                {
                    Id = 4,
                    Question = "Are there group workout programs available?",
                    Answers =
                    {
                        "Yes, we offer group workout programs such as strength training, HIIT, functional fitness, and mobility sessions.",
                        "Our group classes are led by instructors and are designed to suit different fitness levels."
                    }
                },
                new()
                {
                    Id = 5,
                    Question = "Is nutrition guidance included in the plans?",
                    Answers =
                    {
                        "Some membership plans include basic nutrition guidance to help support your training goals.",
                        "For more personalized advice, you can book additional nutrition coaching with one of our specialists."
                    }
                }
            }
        };

        return View("Index", model);
    }
}
