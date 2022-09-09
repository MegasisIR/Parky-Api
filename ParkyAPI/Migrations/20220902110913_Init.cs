using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkyAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'Init'
    public partial class Init : Migration
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'Init'
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'Init.Up(MigrationBuilder)'
        protected override void Up(MigrationBuilder migrationBuilder)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'Init.Up(MigrationBuilder)'
        {
            migrationBuilder.CreateTable(
                name: "NationalParks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Picture = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Established = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NationalParks", x => x.Id);
                });
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'Init.Down(MigrationBuilder)'
        protected override void Down(MigrationBuilder migrationBuilder)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'Init.Down(MigrationBuilder)'
        {
            migrationBuilder.DropTable(
                name: "NationalParks");
        }
    }
}
