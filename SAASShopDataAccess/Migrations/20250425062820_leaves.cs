using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SAASShopDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class leaves : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApprovesType",
                table: "Leaves",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovesType",
                table: "Leaves");
        }
    }
}
