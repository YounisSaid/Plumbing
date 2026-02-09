using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepositoryLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddedOneToManyConfigBetweenCategoryAndPortoflio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Portofolios_Categories_CategoryId",
                table: "Portofolios");

            migrationBuilder.UpdateData(
                table: "Abouts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: "2/9/2026");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: "2/9/2026");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: "2/9/2026");

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: "2/9/2026");

            migrationBuilder.UpdateData(
                table: "HomePages",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: "2/9/2026");

            migrationBuilder.UpdateData(
                table: "Portofolios",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: "2/9/2026");

            migrationBuilder.UpdateData(
                table: "Portofolios",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: "2/9/2026");

            migrationBuilder.UpdateData(
                table: "Portofolios",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: "2/9/2026");

            migrationBuilder.UpdateData(
                table: "Portofolios",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: "2/9/2026");

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: "2/9/2026");

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: "2/9/2026");

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: "2/9/2026");

            migrationBuilder.UpdateData(
                table: "SocialMedias",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: "2/9/2026");

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: "2/9/2026");

            migrationBuilder.UpdateData(
                table: "Testimonials",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: "2/9/2026");

            migrationBuilder.UpdateData(
                table: "Testimonials",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: "2/9/2026");

            migrationBuilder.UpdateData(
                table: "Testimonials",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: "2/9/2026");

            migrationBuilder.AddForeignKey(
                name: "FK_Portofolios_Categories_CategoryId",
                table: "Portofolios",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Portofolios_Categories_CategoryId",
                table: "Portofolios");

            migrationBuilder.UpdateData(
                table: "Abouts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: "11/29/2025");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: "11/29/2025");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: "11/29/2025");

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: "11/29/2025");

            migrationBuilder.UpdateData(
                table: "HomePages",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: "11/29/2025");

            migrationBuilder.UpdateData(
                table: "Portofolios",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: "11/29/2025");

            migrationBuilder.UpdateData(
                table: "Portofolios",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: "11/29/2025");

            migrationBuilder.UpdateData(
                table: "Portofolios",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: "11/29/2025");

            migrationBuilder.UpdateData(
                table: "Portofolios",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: "11/29/2025");

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: "11/29/2025");

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: "11/29/2025");

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: "11/29/2025");

            migrationBuilder.UpdateData(
                table: "SocialMedias",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: "11/29/2025");

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: "11/29/2025");

            migrationBuilder.UpdateData(
                table: "Testimonials",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: "11/29/2025");

            migrationBuilder.UpdateData(
                table: "Testimonials",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: "11/29/2025");

            migrationBuilder.UpdateData(
                table: "Testimonials",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: "11/29/2025");

            migrationBuilder.AddForeignKey(
                name: "FK_Portofolios_Categories_CategoryId",
                table: "Portofolios",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
