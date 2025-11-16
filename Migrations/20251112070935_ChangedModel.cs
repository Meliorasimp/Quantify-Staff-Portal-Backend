using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnterpriseGradeInventoryAPI.Migrations
{
    /// <inheritdoc />
    public partial class ChangedModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "StorageLocations");

            migrationBuilder.DropColumn(
                name: "CurrentOccupancy",
                table: "StorageLocations");

            migrationBuilder.RenameColumn(
                name: "Utilization",
                table: "StorageLocations",
                newName: "MaxCapacity");

            migrationBuilder.RenameColumn(
                name: "UnitOfMeasure",
                table: "StorageLocations",
                newName: "UnitType");

            migrationBuilder.RenameColumn(
                name: "Notes",
                table: "StorageLocations",
                newName: "StorageType");

            migrationBuilder.AddColumn<string>(
                name: "SectionName",
                table: "StorageLocations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SectionName",
                table: "StorageLocations");

            migrationBuilder.RenameColumn(
                name: "UnitType",
                table: "StorageLocations",
                newName: "UnitOfMeasure");

            migrationBuilder.RenameColumn(
                name: "StorageType",
                table: "StorageLocations",
                newName: "Notes");

            migrationBuilder.RenameColumn(
                name: "MaxCapacity",
                table: "StorageLocations",
                newName: "Utilization");

            migrationBuilder.AddColumn<int>(
                name: "Capacity",
                table: "StorageLocations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CurrentOccupancy",
                table: "StorageLocations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
