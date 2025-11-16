using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnterpriseGradeInventoryAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCascadeBehavior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventories_StorageLocations_StorageLocationId",
                table: "Inventories");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventories_Users_UserId",
                table: "Inventories");

            migrationBuilder.DropForeignKey(
                name: "FK_Warehouses_Users_CreatedByUserId",
                table: "Warehouses");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Warehouses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "Inventories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_UserId",
                table: "Warehouses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_UserId1",
                table: "Inventories",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventories_StorageLocations_StorageLocationId",
                table: "Inventories",
                column: "StorageLocationId",
                principalTable: "StorageLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventories_Users_UserId",
                table: "Inventories",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventories_Users_UserId1",
                table: "Inventories",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Warehouses_Users_CreatedByUserId",
                table: "Warehouses",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Warehouses_Users_UserId",
                table: "Warehouses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventories_StorageLocations_StorageLocationId",
                table: "Inventories");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventories_Users_UserId",
                table: "Inventories");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventories_Users_UserId1",
                table: "Inventories");

            migrationBuilder.DropForeignKey(
                name: "FK_Warehouses_Users_CreatedByUserId",
                table: "Warehouses");

            migrationBuilder.DropForeignKey(
                name: "FK_Warehouses_Users_UserId",
                table: "Warehouses");

            migrationBuilder.DropIndex(
                name: "IX_Warehouses_UserId",
                table: "Warehouses");

            migrationBuilder.DropIndex(
                name: "IX_Inventories_UserId1",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Warehouses");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Inventories");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventories_StorageLocations_StorageLocationId",
                table: "Inventories",
                column: "StorageLocationId",
                principalTable: "StorageLocations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventories_Users_UserId",
                table: "Inventories",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Warehouses_Users_CreatedByUserId",
                table: "Warehouses",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
