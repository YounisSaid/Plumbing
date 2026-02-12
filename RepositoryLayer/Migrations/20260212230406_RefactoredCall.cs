using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepositoryLayer.Migrations
{
    /// <inheritdoc />
    public partial class RefactoredCall : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Call",
                table: "Contacts",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(11)",
                oldMaxLength: 11);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "40C9B336-098C-4B39-B039-CB6E5A66803D",
                column: "ConcurrencyStamp",
                value: "358d7bfa-c084-4d05-bfbb-f37394e73cc8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "70552DF1-CB86-4D03-89C3-3DC76CC5B580",
                column: "ConcurrencyStamp",
                value: "bcd4be47-89bc-40c9-82d2-20f6192e607f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "77EBB6A6-7426-4C99-9A5B-F1975438F764",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8b947509-3170-408d-bc93-11f976317839", "AQAAAAIAAYagAAAAEL9urS4qWlIbyQ3zppjXaZupw85ybrq5Kij0L/sVn0pQOFU9rTa/P2te4QbjdeCtmA==", "e1da36b0-1655-4a6d-86cf-069530f69d7a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "B1D61281-1273-4F2F-867F-BE9ADE0377A6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e1d4166e-7b68-4ba3-baa8-b0dcad5f272b", "AQAAAAIAAYagAAAAEJk0Aq/7mJLxf3IxSPyCMCec1+zTqp2f8LXXUn4Mb7J2ybEVY9w5zSb5NXlozaZBIw==", "d6bb09ab-01b4-4786-b5e2-f67a109a94a0" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Call",
                table: "Contacts",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

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
    }
}
