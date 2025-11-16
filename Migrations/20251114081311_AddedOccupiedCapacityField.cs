using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnterpriseGradeInventoryAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddedOccupiedCapacityField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "StorageLocations");

            migrationBuilder.AddColumn<int>(
                name: "OccupiedCapacity",
                table: "StorageLocations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OccupiedCapacity",
                table: "StorageLocations");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "StorageLocations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
