using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Queue_WebApi.Migrations
{
    /// <inheritdoc />
    public partial class CreateDq : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apointsments_Queue_HallId",
                table: "Apointsments");

            migrationBuilder.RenameColumn(
                name: "HallId",
                table: "Apointsments",
                newName: "QueueId");

            migrationBuilder.RenameIndex(
                name: "IX_Apointsments_HallId",
                table: "Apointsments",
                newName: "IX_Apointsments_QueueId");

            migrationBuilder.AddForeignKey(
                name: "FK_Apointsments_Queue_QueueId",
                table: "Apointsments",
                column: "QueueId",
                principalTable: "Queue",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apointsments_Queue_QueueId",
                table: "Apointsments");

            migrationBuilder.RenameColumn(
                name: "QueueId",
                table: "Apointsments",
                newName: "HallId");

            migrationBuilder.RenameIndex(
                name: "IX_Apointsments_QueueId",
                table: "Apointsments",
                newName: "IX_Apointsments_HallId");

            migrationBuilder.AddForeignKey(
                name: "FK_Apointsments_Queue_HallId",
                table: "Apointsments",
                column: "HallId",
                principalTable: "Queue",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
