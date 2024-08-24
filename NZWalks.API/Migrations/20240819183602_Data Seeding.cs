using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class DataSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("4b0ac70b-6f04-4d67-a079-b8177786e4b0"), "Medium" },
                    { new Guid("ab8fcab7-54e6-4a80-b9a8-8fc1ed4e1d1e"), "Hard" },
                    { new Guid("dc9da68d-9bfc-429a-b35d-674b61c4ca47"), "Easy" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("446ea138-b99b-4fff-8317-2b342d7f8bc8"), "AKL", "Auckland", "https://images.pexels.com/photos/5342974/pexels-photo-5342974.jpeg" },
                    { new Guid("591f72b4-4cdb-48aa-ae39-f97a9d039d82"), "WGN", "Wellington", "https://images.pexels.com/photos/10116189/pexels-photo-10116189.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2" },
                    { new Guid("6798d67e-fb34-40b5-abe7-cb8d8528aada"), "NTL", "Northland", null },
                    { new Guid("72e220ba-b323-4201-8715-674b7fa87c81"), "NSN", "Nelson", "https://images.pexels.com/photos/20788199/pexels-photo-20788199/free-photo-of-place-jacques-cartier-with-nelsons-column-in-montreal.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2" },
                    { new Guid("8a26a4c2-5649-4f6c-b5a3-25b30708ae64"), "BOP", "Bay Of Plenty", null },
                    { new Guid("f8e28077-244a-4122-a26e-9344a3fbdcd6"), "STL", "Southland", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("4b0ac70b-6f04-4d67-a079-b8177786e4b0"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("ab8fcab7-54e6-4a80-b9a8-8fc1ed4e1d1e"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("dc9da68d-9bfc-429a-b35d-674b61c4ca47"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("446ea138-b99b-4fff-8317-2b342d7f8bc8"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("591f72b4-4cdb-48aa-ae39-f97a9d039d82"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("6798d67e-fb34-40b5-abe7-cb8d8528aada"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("72e220ba-b323-4201-8715-674b7fa87c81"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("8a26a4c2-5649-4f6c-b5a3-25b30708ae64"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("f8e28077-244a-4122-a26e-9344a3fbdcd6"));
        }
    }
}
