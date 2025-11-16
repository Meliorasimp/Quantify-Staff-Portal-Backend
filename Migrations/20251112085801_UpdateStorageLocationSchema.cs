using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnterpriseGradeInventoryAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateStorageLocationSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "StorageLocations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "StorageLocations");
        }
    }
}
