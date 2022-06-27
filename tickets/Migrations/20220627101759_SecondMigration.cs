using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace tickets.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "birth_date",
                table: "segments",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "passenger_type",
                table: "segments",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "birth_date",
                table: "segments");

            migrationBuilder.DropColumn(
                name: "passenger_type",
                table: "segments");
        }
    }
}
