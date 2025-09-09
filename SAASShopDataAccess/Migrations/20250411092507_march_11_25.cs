using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SAASShopDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class march_11_25 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LeaveBalances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CasualLeaveBalance = table.Column<int>(type: "int", nullable: false),
                    SickLeaveBalance = table.Column<int>(type: "int", nullable: false),
                    BonusLeaveGiven = table.Column<int>(type: "int", nullable: false),
                    AppUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveBalances", x => x.Id);
                });

           

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeaveBalances");

        }
    }
}
