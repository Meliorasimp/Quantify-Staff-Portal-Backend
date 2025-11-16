using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnterpriseGradeInventoryAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddedRackLocationField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RackLocation",
                table: "Inventories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RackLocation",
                table: "Inventories");
        }
    }
}
