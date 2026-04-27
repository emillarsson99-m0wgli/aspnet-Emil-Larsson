using CoreFitnessClub.Application.Interfaces;
using CoreFitnessClub.Domain.Entities;

namespace CoreFitnessClub.Application.Services;

public class MembershipService : IMembershipService
{
    private readonly IMembershipRepo _membershipRepo;
    public MembershipService(IMembershipRepo membershipRepo)
    {
        _membershipRepo = membershipRepo;
    }
    public async Task<Membership?> GetByUserIdAsync(string userId)
    {
        return await _membershipRepo.GetByUserIdAsync(userId);
    }

    public async Task CreateAsync(string userId, string membershipType)
    {
        var existingMembership = await _membershipRepo.GetByUserIdAsync(userId);

        if (existingMembership != null)
            return;

        var membership = new Membership
        {
            UserId = userId,
            MembershipType = membershipType,
            Status = "Active",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddMonths(1)
        };

        await _membershipRepo.AddAsync(membership);
    }
}
