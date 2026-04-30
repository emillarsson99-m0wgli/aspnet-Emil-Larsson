using AspNetCoreGeneratedDocument;
using CoreFitnessClub.Application.Interfaces;
using CoreFitnessClub.Infrastructure.Identity;
using CoreFitnessClub.Web.ViewModels;
using CoreFitnessClub.Web.ViewModels.Memberships;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoreFitnessClub.Web.Controllers
{
    [Authorize]
    public class MembershipsController : Controller
    {
        private readonly IMembershipService _membershipService;
        private readonly UserManager<ApplicationUser> _userManager;

        public MembershipsController(IMembershipService membershipService, UserManager<ApplicationUser> userManager)
        {
            _membershipService = membershipService;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> MyMembership()
        {
            var userId = _userManager.GetUserId(User);

            if (userId == null)
                return Challenge();

            var membership = await _membershipService.GetByUserIdAsync(userId);

            return View(membership);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var userId = _userManager.GetUserId(User);

            if (userId == null)
                return Challenge();

            var membership = await _membershipService.GetByUserIdAsync(userId);

            var model = BuildCreateMembershipViewModel();

            if (membership != null)
            {
                model.Membership = new MembershipDetailsViewModel
                {
                    MembershipType = membership.MembershipType,
                    Status = membership.Status,
                    StartDate = membership.StartDate,
                    EndDate = membership.EndDate
                };
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateMembershipViewModel model)
        {
            var userId = _userManager.GetUserId(User);

            if (userId == null)
                return Challenge();

            var existingMembership = await _membershipService.GetByUserIdAsync(userId);

            if (existingMembership != null)
            {
                TempData["ErrorMessage"] = "You already have an active membership.";
                return RedirectToAction(nameof(Create));
            }

            if (!ModelState.IsValid)
            {
                var pageModel = BuildCreateMembershipViewModel();
                pageModel.Type = model.Type;
                return View(pageModel);
            }

            await _membershipService.CreateAsync(userId, model.Type);

            TempData["SuccessMessage"] = "Membership created successfully!";
            return RedirectToAction(nameof(MyMembership));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CancelMembership()
        {
            var userId = _userManager.GetUserId(User);

            if (userId == null)
                return Challenge();

            var success = await _membershipService.DeleteAsync(userId);

            if (!success)
            {
                TempData["ErrorMessage"] = "Membership could not be removed.";
                return RedirectToAction(nameof(MyMembership));
            }

            TempData["SuccessMessage"] = "Your membership has been cancelled successfully.";
            return RedirectToAction(nameof(MyMembership));
        }

        private CreateMembershipViewModel BuildCreateMembershipViewModel()
        {
            return new CreateMembershipViewModel
            {
                MembershipsOffers = new List<MembershipsOfferViewmodel>
                {
                    new()
                        {
                        Type = "Standard",
                        Title = "Standard Membership",
                        Description = "With the Standard Membership, get access to our full range of gym facilities.",
                        Price = 495.00m,
                        MonthlyClasses = 20,
                        Benefits =
                        {
                            "Standard Locker",
                            "High-energy group fitness classes",
                            "Motivating & supportive environment"
                        }
                    },
                    new()
                    {
                        Type = "Premium",
                        Title = "Premium Membership",
                        Description = "With the Premium Membership, get access to our full range of gym facilities.",
                        Price = 595.00m,
                        MonthlyClasses = 20,
                        Benefits =
                        {
                            "Priority Support & Premium Locker",
                            "High-energy group fitness classes",
                            "Motivating & supportive environment"
                        }
                    }
                },

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
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
