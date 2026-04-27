using CoreFitnessClub.Application.Interfaces;
using CoreFitnessClub.Domain.Entities;
using CoreFitnessClub.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CoreFitnessClub.Infrastructure.Repos;

public class MembershipRepo : IMembershipRepo
{
    private readonly ApplicationDbContext _context;

    public MembershipRepo(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Membership?> GetByUserIdAsync(string userId)
    {
       return await _context.Membership
            .FirstOrDefaultAsync(m => m.UserId == userId);
    }

    public async Task AddAsync(Membership membership)
    {
        _context.Membership.Add(membership);
        await _context.SaveChangesAsync();
    }
}
