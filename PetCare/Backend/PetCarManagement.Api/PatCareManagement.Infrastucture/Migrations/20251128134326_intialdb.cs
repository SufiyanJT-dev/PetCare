using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetCareManagement.Infrastucture.Migrations
{
    /// <inheritdoc />
    public partial class intialdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_eventAttachments_MedicalEvents_MedicalEventEventId",
                table: "eventAttachments");

            migrationBuilder.RenameColumn(
                name: "EventId",
                table: "MedicalEvents",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "MedicalEventEventId",
                table: "eventAttachments",
                newName: "MedicalEventId");

            migrationBuilder.RenameIndex(
                name: "IX_eventAttachments_MedicalEventEventId",
                table: "eventAttachments",
                newName: "IX_eventAttachments_MedicalEventId");

            migrationBuilder.AddForeignKey(
                name: "FK_eventAttachments_MedicalEvents_MedicalEventId",
                table: "eventAttachments",
                column: "MedicalEventId",
                principalTable: "MedicalEvents",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_eventAttachments_MedicalEvents_MedicalEventId",
                table: "eventAttachments");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "MedicalEvents",
                newName: "EventId");

            migrationBuilder.RenameColumn(
                name: "MedicalEventId",
                table: "eventAttachments",
                newName: "MedicalEventEventId");

            migrationBuilder.RenameIndex(
                name: "IX_eventAttachments_MedicalEventId",
                table: "eventAttachments",
                newName: "IX_eventAttachments_MedicalEventEventId");

            migrationBuilder.AddForeignKey(
                name: "FK_eventAttachments_MedicalEvents_MedicalEventEventId",
                table: "eventAttachments",
                column: "MedicalEventEventId",
                principalTable: "MedicalEvents",
                principalColumn: "EventId");
        }
    }
}
