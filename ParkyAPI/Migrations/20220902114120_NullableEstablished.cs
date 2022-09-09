using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkyAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'NullableEstablished'
    public partial class NullableEstablished : Migration
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'NullableEstablished'
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'NullableEstablished.Up(MigrationBuilder)'
        protected override void Up(MigrationBuilder migrationBuilder)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'NullableEstablished.Up(MigrationBuilder)'
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Established",
                table: "NationalParks",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'NullableEstablished.Down(MigrationBuilder)'
        protected override void Down(MigrationBuilder migrationBuilder)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'NullableEstablished.Down(MigrationBuilder)'
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Established",
                table: "NationalParks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
