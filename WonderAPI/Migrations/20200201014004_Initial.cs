using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace WonderAPI.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Member",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 500, nullable: false),
                    Email = table.Column<string>(maxLength: 255, nullable: false),
                    OptionalEmail = table.Column<string>(maxLength: 255, nullable: true),
                    Password = table.Column<string>(maxLength: 1000, nullable: false),
                    MobileNumber = table.Column<string>(maxLength: 24, nullable: false),
                    Gender = table.Column<string>(maxLength: 10, nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Member");
        }
    }
}
