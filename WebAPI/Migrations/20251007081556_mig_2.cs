using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class mig_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "571b87d9-cdea-4a20-b474-2ea4db00d253", null, "Admin", "ADMIN" },
                    { "8c52cc50-cf0b-406c-8a99-c12dc8a62af0", null, "User", "USER" },
                    { "fec032ff-6f70-4431-8a3d-e395db61f173", null, "Editor", "EDITOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "571b87d9-cdea-4a20-b474-2ea4db00d253");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8c52cc50-cf0b-406c-8a99-c12dc8a62af0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fec032ff-6f70-4431-8a3d-e395db61f173");
        }
    }
}
