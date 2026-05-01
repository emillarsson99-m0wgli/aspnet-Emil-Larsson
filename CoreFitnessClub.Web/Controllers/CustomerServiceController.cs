using Microsoft.AspNetCore.Mvc;

namespace CoreFitnessClub.Web.Controllers
{
    public class CustomerServiceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
