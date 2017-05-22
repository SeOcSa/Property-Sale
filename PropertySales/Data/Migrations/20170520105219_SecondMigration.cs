using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "urlPhoto",
                table: "PropertyInfos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "urlPhoto2",
                table: "PropertyInfos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "urlPhoto3",
                table: "PropertyInfos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "urlPhoto4",
                table: "PropertyInfos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "urlPhoto",
                table: "PropertyInfos");

            migrationBuilder.DropColumn(
                name: "urlPhoto2",
                table: "PropertyInfos");

            migrationBuilder.DropColumn(
                name: "urlPhoto3",
                table: "PropertyInfos");

            migrationBuilder.DropColumn(
                name: "urlPhoto4",
                table: "PropertyInfos");
        }
    }
}
