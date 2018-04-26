using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GetToTheShopper.WebApi.Migrations
{
    public partial class AddedShopDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Shops",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Lat",
                table: "Shops",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Lng",
                table: "Shops",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Shops",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Shops");

            migrationBuilder.DropColumn(
                name: "Lat",
                table: "Shops");

            migrationBuilder.DropColumn(
                name: "Lng",
                table: "Shops");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Shops");
        }
    }
}
