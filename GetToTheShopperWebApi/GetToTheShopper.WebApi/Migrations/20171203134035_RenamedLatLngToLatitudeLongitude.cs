using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GetToTheShopper.WebApi.Migrations
{
    public partial class RenamedLatLngToLatitudeLongitude : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Lng",
                table: "Shops",
                newName: "Longitude");

            migrationBuilder.RenameColumn(
                name: "Lat",
                table: "Shops",
                newName: "Latitude");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Longitude",
                table: "Shops",
                newName: "Lng");

            migrationBuilder.RenameColumn(
                name: "Latitude",
                table: "Shops",
                newName: "Lat");
        }
    }
}
