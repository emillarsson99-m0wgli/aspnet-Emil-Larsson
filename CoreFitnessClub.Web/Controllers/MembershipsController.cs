using CoreFitnessClub.Application.Interfaces;
using CoreFitnessClub.Infrastructure.Identity;
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

            var existingMembership = await _membershipService.GetByUserIdAsync(userId);

            if (existingMembership == null)
            {
                TempData["ErrorMessage"] = "You already have an active membership.";
                return RedirectToAction("MyMembership");
            }

            return View(new CreateMembershipViewModel());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateMembershipViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var userId = _userManager.GetUserId(User);

            if (userId == null)
                return Challenge();

            var existingMembership = await _membershipService.GetByUserIdAsync(userId);

            if (existingMembership == null)
            {
                ModelState.AddModelError(string.Empty, "You already have an active membership.");
                return View(model);
            }

            await _membershipService.CreateAsync(userId, model.Type);

            TempData["SuccessMessage"] = "Membership created successfully!";
            return RedirectToAction(nameof(MyMembership));
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
