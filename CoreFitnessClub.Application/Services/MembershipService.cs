using CoreFitnessClub.Application.Interfaces;
using CoreFitnessClub.Domain.Entities;

namespace CoreFitnessClub.Application.Services;

public class MembershipService(IMembershipRepo membershipRepo) : IMembershipService
{
    private readonly IMembershipRepo _membershipRepo = membershipRepo;

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

    public async Task<bool> DeleteAsync(string userId)
    {
        var membership = await _membershipRepo.GetByUserIdAsync(userId);

        if (membership == null)
            return false;

        await _membershipRepo.DeleteAsync(membership);

        return true;
    }
}
