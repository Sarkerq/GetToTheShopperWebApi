using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GetToTheShopper.WebApi.Migrations
{
    public partial class Changed_userId_type_to_string : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SharedReceipts_AspNetUsers_ProductId",
                table: "SharedReceipts");

            migrationBuilder.DropIndex(
                name: "IX_SharedReceipts_ProductId",
                table: "SharedReceipts");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "SharedReceipts");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "SharedReceipts",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "AuthorId",
                table: "Receipts",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_SharedReceipts_UserId",
                table: "SharedReceipts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SharedReceipts_AspNetUsers_UserId",
                table: "SharedReceipts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SharedReceipts_AspNetUsers_UserId",
                table: "SharedReceipts");

            migrationBuilder.DropIndex(
                name: "IX_SharedReceipts_UserId",
                table: "SharedReceipts");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "SharedReceipts",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductId",
                table: "SharedReceipts",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AuthorId",
                table: "Receipts",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SharedReceipts_ProductId",
                table: "SharedReceipts",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_SharedReceipts_AspNetUsers_ProductId",
                table: "SharedReceipts",
                column: "ProductId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
