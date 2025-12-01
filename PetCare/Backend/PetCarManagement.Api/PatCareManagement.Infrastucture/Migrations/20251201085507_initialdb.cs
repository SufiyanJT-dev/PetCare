using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetCareManagement.Infrastucture.Migrations
{
    /// <inheritdoc />
    public partial class initialdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reminders_Pets_PetsPetId",
                table: "Reminders");

            migrationBuilder.DropIndex(
                name: "IX_Reminders_PetsPetId",
                table: "Reminders");

            migrationBuilder.RenameColumn(
                name: "PetsPetId",
                table: "Reminders",
                newName: "LinkedEntityId");

            migrationBuilder.AddColumn<string>(
                name: "JobId",
                table: "Reminders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SourceType",
                table: "Reminders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Reminders_PetId",
                table: "Reminders",
                column: "PetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reminders_Pets_PetId",
                table: "Reminders",
                column: "PetId",
                principalTable: "Pets",
                principalColumn: "PetId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reminders_Pets_PetId",
                table: "Reminders");

            migrationBuilder.DropIndex(
                name: "IX_Reminders_PetId",
                table: "Reminders");

            migrationBuilder.DropColumn(
                name: "JobId",
                table: "Reminders");

            migrationBuilder.DropColumn(
                name: "SourceType",
                table: "Reminders");

            migrationBuilder.RenameColumn(
                name: "LinkedEntityId",
                table: "Reminders",
                newName: "PetsPetId");

            migrationBuilder.CreateIndex(
                name: "IX_Reminders_PetsPetId",
                table: "Reminders",
                column: "PetsPetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reminders_Pets_PetsPetId",
                table: "Reminders",
                column: "PetsPetId",
                principalTable: "Pets",
                principalColumn: "PetId");
        }
    }
}
