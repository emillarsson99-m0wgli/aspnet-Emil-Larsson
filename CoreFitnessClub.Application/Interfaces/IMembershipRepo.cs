using CoreFitnessClub.Domain.Entities;

namespace CoreFitnessClub.Application.Interfaces;

public interface IMembershipRepo
{
    Task<Membership?> GetByUserIdAsync(string userId);
    Task AddAsync(Membership membership);
}
