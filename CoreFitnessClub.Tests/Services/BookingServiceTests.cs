using CoreFitnessClub.Application.Interfaces;
using CoreFitnessClub.Application.Services;
using CoreFitnessClub.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFitnessClub.Tests.Services;

public class BookingServiceTests
{
    [Fact]
    public async Task BookClassAssync_ReturnsFalse_WhenWorkoutClassDoesNotExist()
    {
        var bookingRepo = new Mock<IBookingRepo>();
        var workoutClassRepo = new Mock<IWorkoutClassRepo>();

        workoutClassRepo.Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync((WorkoutClass?)null);

        var service = new BookingService(bookingRepo.Object, workoutClassRepo.Object);

        var result = await service.BookClassAsync("user1", 1);

        Assert.False(result);
        bookingRepo.Verify(x => x.AddAsync(It.IsAny<Booking>()), Times.Never);
    }

    [Fact]
    public async Task BookClassAsync_ReturnsFalse_WhenUserAlreadyBookedClass()
    {
        var bookingRepo = new Mock<IBookingRepo>();
        var workoutClassRepo = new Mock<IWorkoutClassRepo>();

        workoutClassRepo.Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync(new WorkoutClass { Id = 1, Name = "Yoga" });

        bookingRepo.Setup(x => x.ExistsAsync("user1", 1))
            .ReturnsAsync(true);

        var service = new BookingService(bookingRepo.Object, workoutClassRepo.Object);

        var result = await service.BookClassAsync("user1", 1);

        Assert.False(result);
        bookingRepo.Verify(x => x.AddAsync(It.IsAny<Booking>()), Times.Never);
    }

    [Fact]
    public async Task BookClassAsync_AddsBooking_WhenClassExistsAndNotAlreadyBooked()
    {
        var bookingRepo = new Mock<IBookingRepo>();
        var workoutClassRepo = new Mock<IWorkoutClassRepo>();

        workoutClassRepo.Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync(new WorkoutClass { Id = 1, Name = "Yoga" });

        bookingRepo.Setup(x => x.ExistsAsync("user1", 1))
            .ReturnsAsync(false);

        var service = new BookingService(bookingRepo.Object, workoutClassRepo.Object);

        var result = await service.BookClassAsync("user1", 1);

        Assert.True(result);
        bookingRepo.Verify(x => x.AddAsync(It.Is<Booking>(b => b.UserId == "user1" && b.WorkoutClass.Id == 1)), Times.Once);
    }
}
