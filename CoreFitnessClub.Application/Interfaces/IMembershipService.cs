using CoreFitnessClub.Domain.Entities;

namespace CoreFitnessClub.Application.Interfaces;

public interface IMembershipService
{
    Task<Membership?> GetByUserIdAsync(string userId);
    Task CreateAsync (string userId, string membershipType);
}
