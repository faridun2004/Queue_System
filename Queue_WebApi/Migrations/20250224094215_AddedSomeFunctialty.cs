using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Queue_WebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddedSomeFunctialty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apointsments_Queue_QueueId",
                table: "Apointsments");

            migrationBuilder.DropForeignKey(
                name: "FK_Queue_Halls_HallId",
                table: "Queue");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Queue",
                table: "Queue");

            migrationBuilder.RenameTable(
                name: "Queue",
                newName: "Queues");

            migrationBuilder.RenameIndex(
                name: "IX_Queue_HallId",
                table: "Queues",
                newName: "IX_Queues_HallId");

            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "Queues",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Queues",
                table: "Queues",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Apointsments_Queues_QueueId",
                table: "Apointsments",
                column: "QueueId",
                principalTable: "Queues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Queues_Halls_HallId",
                table: "Queues",
                column: "HallId",
                principalTable: "Halls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apointsments_Queues_QueueId",
                table: "Apointsments");

            migrationBuilder.DropForeignKey(
                name: "FK_Queues_Halls_HallId",
                table: "Queues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Queues",
                table: "Queues");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "Queues");

            migrationBuilder.RenameTable(
                name: "Queues",
                newName: "Queue");

            migrationBuilder.RenameIndex(
                name: "IX_Queues_HallId",
                table: "Queue",
                newName: "IX_Queue_HallId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Queue",
                table: "Queue",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Apointsments_Queue_QueueId",
                table: "Apointsments",
                column: "QueueId",
                principalTable: "Queue",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Queue_Halls_HallId",
                table: "Queue",
                column: "HallId",
                principalTable: "Halls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
