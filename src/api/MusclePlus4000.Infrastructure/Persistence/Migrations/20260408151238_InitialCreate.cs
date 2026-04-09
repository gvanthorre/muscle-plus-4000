using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MusclePlus4000.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "app");

            migrationBuilder.CreateTable(
                name: "Exercises",
                schema: "app",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercises", x => x.Id);
                });

            migrationBuilder.InsertData(
                schema: "app",
                table: "Exercises",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Description", "LastModifiedAt", "LastModifiedBy", "Name" },
                values: new object[,]
                {
                    { new Guid("018f2a3e-4f7a-7b24-b78f-8d1ec2dc0001"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Lie on a bench and push weights", null, null, "Bench Press" },
                    { new Guid("018f2a3e-4f7a-7b24-b78f-8d1ec2dc0002"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Take weights on your shoulders, go down then up", null, null, "Back Squat" },
                    { new Guid("018f2a3e-4f7a-7b24-b78f-8d1ec2dc0003"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Grab a bar above yourself and get up", null, null, "Pull Up" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Exercises",
                schema: "app");
        }
    }
}
