using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnterpriseGradeInventoryAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddStorageLocationUserRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StorageLocations_Users_UserId",
                table: "StorageLocations");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "StorageLocations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StorageLocations_Users_UserId",
                table: "StorageLocations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StorageLocations_Users_UserId",
                table: "StorageLocations");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "StorageLocations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_StorageLocations_Users_UserId",
                table: "StorageLocations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
