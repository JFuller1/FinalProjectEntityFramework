using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProjectEntityFramework.Data.Migrations
{
    public partial class SeedingData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "1a", 0, "afe3a347-667a-4896-bc91-94694d0aace5", "email@gmail.com", false, "Jaeden", "Fuller", false, null, null, null, null, null, false, "aa16fa05-bdeb-48cc-a9aa-a7b4971db319", false, null },
                    { "2a", 0, "d71f2916-09a1-47a0-a705-b4e21f94df0f", "unoriginal@email.com", false, "John", "Doe", false, null, null, null, null, null, false, "933e899b-976d-4092-a9e7-8787d8f72053", false, null },
                    { "3a", 0, "44dfde02-c43e-44db-ae92-6069e80ca4ed", "moreunoriginal@email.com", false, "Jane", "Doe", false, null, null, null, null, null, false, "bb8f3e3a-54d6-4731-b7ef-ff0e819d0220", false, null }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Cleaning" },
                    { 2, "Shopping" },
                    { 3, "Finances" },
                    { 4, "Yardwork" }
                });

            migrationBuilder.InsertData(
                table: "Chores",
                columns: new[] { "Id", "CategoryId", "ChoreType", "ChoreUserId", "IsComplete", "Name" },
                values: new object[,]
                {
                    { 3, null, 0, null, false, "Pay ticket" },
                    { 7, null, 1, null, false, "Eat dinner" }
                });

            migrationBuilder.InsertData(
                table: "ChoreMonths",
                columns: new[] { "Id", "ChoreId", "Month" },
                values: new object[] { 3, 3, 8 });

            migrationBuilder.InsertData(
                table: "Chores",
                columns: new[] { "Id", "CategoryId", "ChoreType", "ChoreUserId", "IsComplete", "Name" },
                values: new object[,]
                {
                    { 1, 1, 2, "1a", false, "Do dishes" },
                    { 2, 3, 4, "2a", false, "Do taxes" },
                    { 4, 1, 1, "2a", false, "Tidy the house" },
                    { 5, 1, 3, "3a", false, "Vacuum house" },
                    { 6, 2, 5, "1a", false, "Stock up for the year" },
                    { 8, 2, 2, "1a", false, "Buy groceries" },
                    { 9, 4, 4, "3a", false, "Shovel" },
                    { 10, 3, 3, "1a", false, "Pay car insurance" }
                });

            migrationBuilder.InsertData(
                table: "ChoreMonths",
                columns: new[] { "Id", "ChoreId", "Month" },
                values: new object[,]
                {
                    { 1, 2, 0 },
                    { 2, 2, 6 },
                    { 4, 6, 0 },
                    { 5, 9, 0 },
                    { 6, 9, 1 },
                    { 7, 9, 2 },
                    { 8, 9, 10 },
                    { 9, 9, 11 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ChoreMonths",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ChoreMonths",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ChoreMonths",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ChoreMonths",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ChoreMonths",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ChoreMonths",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ChoreMonths",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ChoreMonths",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ChoreMonths",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Chores",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Chores",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Chores",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Chores",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Chores",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Chores",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Chores",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Chores",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Chores",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Chores",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1a");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2a");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3a");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
