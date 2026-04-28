using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CoreFitnessClub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedWorkoutClasses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "WorkoutClasses",
                columns: new[] { "Id", "Capacity", "Category", "Date", "EndTime", "Instructor", "Name", "StartTime" },
                values: new object[,]
                {
                    { 1, 20, "Mind & Body", new DateTime(2024, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 18, 0, 0, 0), "Alice Smith", "Yoga", new TimeSpan(0, 17, 0, 0, 0) },
                    { 2, 10, "Strength", new DateTime(2024, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 19, 0, 0, 0), "Phil Heath", "Strength training", new TimeSpan(0, 17, 0, 0, 0) },
                    { 3, 10, "Grappling", new DateTime(2024, 7, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 19, 0, 0, 0), "John Danaher", "BJJ", new TimeSpan(0, 17, 0, 0, 0) },
                    { 4, 10, "Striking", new DateTime(2024, 7, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 19, 0, 0, 0), "Alex Pereira", "Kickboxing", new TimeSpan(0, 17, 0, 0, 0) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "WorkoutClasses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "WorkoutClasses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "WorkoutClasses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "WorkoutClasses",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
