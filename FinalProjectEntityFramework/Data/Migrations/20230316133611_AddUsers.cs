using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProjectEntityFramework.Data.Migrations
{
    public partial class AddUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1a",
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "2bf438f1-1d34-404c-9377-3626b47d97b8", true, "EMAIL@GMAIL.COM", "AQAAAAEAACcQAAAAEBHWOQIzfP+5z2TosKujlDhhdJwlqK1Jf9VeMaLggOXpSPT5XKqMC/wF5gl1oohWmw==", "d57ed745-b53d-4015-ae22-104b85072966", "email@gmail.com" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2a",
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "b1f7ebe7-95cd-497f-aff9-79b79626a390", true, "UNORIGINAL@EMAIL.COM", "AQAAAAEAACcQAAAAEDVcdX4DGss1RWAwQWkV1D86jbOym3ZQltY5tDuey6Uf4QpBYCGl3ZVpX+dV0Eu2pQ==", "3f3cc2d1-0ffb-4c69-94cf-b7c7ab622c01", "unoriginal@email.com" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3a",
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "fe58968c-15d0-4867-b78f-46e9b2d22231", true, "MOREUNORIGINAL@EMAIL.COM", "AQAAAAEAACcQAAAAEPjlfSV7rbweMlM5DRtAQYDcqZx6npBUMiWWOavNlF8qacXgpVyI3Z5y9JqBybvvag==", "4a731897-ff1c-4c58-95ff-cb623361da98", "moreunoriginal@email.com" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1a",
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "afe3a347-667a-4896-bc91-94694d0aace5", false, null, null, "aa16fa05-bdeb-48cc-a9aa-a7b4971db319", null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2a",
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "d71f2916-09a1-47a0-a705-b4e21f94df0f", false, null, null, "933e899b-976d-4092-a9e7-8787d8f72053", null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3a",
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "44dfde02-c43e-44db-ae92-6069e80ca4ed", false, null, null, "bb8f3e3a-54d6-4731-b7ef-ff0e819d0220", null });
        }
    }
}
