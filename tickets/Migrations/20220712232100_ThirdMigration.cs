using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace tickets.Migrations
{
    public partial class ThirdMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "operation_place",
                table: "segments",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "operation_time",
                table: "segments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<short>(
                name: "operation_time_timezone",
                table: "segments",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "operation_place",
                table: "segments");

            migrationBuilder.DropColumn(
                name: "operation_time",
                table: "segments");

            migrationBuilder.DropColumn(
                name: "operation_time_timezone",
                table: "segments");
        }
    }
}
