using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Queue_WebApi.Migrations
{
    /// <inheritdoc />
    public partial class SeedFirstAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var groupId = new Guid("11111111-1111-1111-1111-111111111111");
            var adminId = new Guid("22222222-2222-2222-2222-222222222222");
            var createdAt = new DateTime(
                2026, 11, 26, 10, 0, 0,
                DateTimeKind.Utc);

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[]
                {
            "Id", "Name", "CreatedAt", "UpdatedAt",
            "CreatedByUserId", "UpdatedByUserId"
                },
                values: new object[]
                {
            groupId, "Default", createdAt, createdAt,
            Guid.Empty, null!
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[]
                {
            "Id", "Name", "KeyWord", "GroupId",
            "Username", "Password", "RefreshToken",
            "Role", "IsDeleted", "CreatedAt", "UpdatedAt",
            "CreatedByUserId", "UpdatedByUserId"
                },
                values: new object[]
                {
            adminId,
            "Administrator",
            "admin",
            groupId,
            "admin",
            "$2a$11$bEJs8Z.IKfWWwXZUKrVfXefo5.OIdoHzGjv6Naw8Kd.o4uW8Msc8S",
            null!,
            "admin",
            false,
            createdAt,
            createdAt,
            Guid.Empty,
            null!
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));
        }
    }
}
