using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BusinessLogic_Layer.Migrations
{
    /// <inheritdoc />
    public partial class updateStockandcomment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9266b677-979d-4013-bbc0-3d8fb2f6dafc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fc127e44-ac3f-4a70-b5a2-290f6a74ef16");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Stocks",
                newName: "StockId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Comments",
                newName: "CommentId");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "29784185-6c50-43a8-a958-0127e15fc414", null, "Admin", "ADMIN" },
                    { "ba1639d7-3ac5-4001-b147-a51da3a0e454", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "29784185-6c50-43a8-a958-0127e15fc414");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ba1639d7-3ac5-4001-b147-a51da3a0e454");

            migrationBuilder.RenameColumn(
                name: "StockId",
                table: "Stocks",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CommentId",
                table: "Comments",
                newName: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "9266b677-979d-4013-bbc0-3d8fb2f6dafc", null, "Admin", "ADMIN" },
                    { "fc127e44-ac3f-4a70-b5a2-290f6a74ef16", null, "User", "USER" }
                });
        }
    }
}
