using CoreFitnessClub.Application.Interfaces;
using CoreFitnessClub.Application.Services;
using CoreFitnessClub.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFitnessClub.Tests.Services;

public class MembershipServiceTests
{
    [Fact]
    public async Task CreateAsync_DoesNotAddMembership_WhenUserAlreadyHasMembership()
    {
        var membershipRepo = new Mock<IMembershipRepo>();

        membershipRepo.Setup(x => x.GetByUserIdAsync("User1"))
           .ReturnsAsync(new Membership
           {
               Id = 1,
               UserId = "User1",
               MembershipType = "Standard",
               Status = "Active"
           });

        var service = new MembershipService(membershipRepo.Object);

         await service.CreateAsync("User1", "Premium");

        membershipRepo.Verify(x => x.AddAsync(It.IsAny<Membership>()), Times.Never);
    }
}
