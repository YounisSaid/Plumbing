using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepositoryLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddClaim : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUserClaims",
                type: "nvarchar(34)",
                maxLength: 34,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "40C9B336-098C-4B39-B039-CB6E5A66803D",
                column: "ConcurrencyStamp",
                value: "b81bf9ba-13c1-447e-8011-98488181b9a9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "70552DF1-CB86-4D03-89C3-3DC76CC5B580",
                column: "ConcurrencyStamp",
                value: "91ab32d5-92ab-4cb9-b32e-e6e09a383715");

            migrationBuilder.InsertData(
                table: "AspNetUserClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "Discriminator", "UserId" },
                values: new object[] { 1, "AdminObserverExpireDate", "12/2/2026", "AppUserClaim", "77EBB6A6-7426-4C99-9A5B-F1975438F764" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "77EBB6A6-7426-4C99-9A5B-F1975438F764",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f60152af-8c07-41b7-862b-9721ab1cf53f", "AQAAAAIAAYagAAAAEC4Qx2S0NOUQiVhfM+T4JZYKUqpB8+5Q5/IwkfP8lbHKBWAHZfTKCWKloMYaWrefkQ==", "8386f4f7-3f7d-4746-906a-c2b688711878" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "B1D61281-1273-4F2F-867F-BE9ADE0377A6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7dd65f8a-0e04-4d2b-b331-6be5eb5419f6", "AQAAAAIAAYagAAAAEMbUKIj3QyUMoUrMXXKn719DkGb6tqFsp6z9vadUV4ebwRAwapIxjUhg5PgwfxZRXw==", "ba00a522-bf48-49c6-a0da-a2ea66e5fb04" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUserClaims");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "40C9B336-098C-4B39-B039-CB6E5A66803D",
                column: "ConcurrencyStamp",
                value: "29d7bf33-c89a-4ed6-8a32-48921a05c9cc");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "70552DF1-CB86-4D03-89C3-3DC76CC5B580",
                column: "ConcurrencyStamp",
                value: "2a1c673c-90d4-42a2-9017-644915150dee");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "77EBB6A6-7426-4C99-9A5B-F1975438F764",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "aab90318-51d0-4703-a03d-783c86c6acaa", "AQAAAAIAAYagAAAAEAqEzHw+p5kUp9TeaXBhcDL9AS/tzXNJ8ETYmdkIz3AQUHnJVgzm3qkAfwIGWVe+WA==", "9189242b-0d68-4065-96e9-fbfc9e018f23" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "B1D61281-1273-4F2F-867F-BE9ADE0377A6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "10cb05c4-0193-42c3-9f47-f7658a585b92", "AQAAAAIAAYagAAAAEFHuBNttWSByS+COjXn1P3IECIHtycMTRBJicjtxbuge5RXcpI5/ivf2XrOdwImdJg==", "26bc20fb-5870-4d7f-843a-57ccef73198b" });
        }
    }
}
