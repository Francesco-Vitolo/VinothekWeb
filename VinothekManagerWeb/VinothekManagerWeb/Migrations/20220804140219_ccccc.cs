using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VinothekManagerWeb.Migrations
{
    public partial class ccccc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Image_ImageId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Producer_ProducerModelProducerId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_ProducerModelProducerId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ProducerModelProducerId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Blob",
                table: "Image");

            migrationBuilder.RenameColumn(
                name: "ImageId",
                table: "Product",
                newName: "ProducerId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_ImageId",
                table: "Product",
                newName: "IX_Product_ProducerId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Image",
                newName: "FilePath");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Image",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Image_ProductId",
                table: "Image",
                column: "ProductId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Image_Product_ProductId",
                table: "Image",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Producer_ProducerId",
                table: "Product",
                column: "ProducerId",
                principalTable: "Producer",
                principalColumn: "ProducerId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Image_Product_ProductId",
                table: "Image");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Producer_ProducerId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Image_ProductId",
                table: "Image");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Image");

            migrationBuilder.RenameColumn(
                name: "ProducerId",
                table: "Product",
                newName: "ImageId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_ProducerId",
                table: "Product",
                newName: "IX_Product_ImageId");

            migrationBuilder.RenameColumn(
                name: "FilePath",
                table: "Image",
                newName: "Name");

            migrationBuilder.AddColumn<int>(
                name: "ProducerModelProducerId",
                table: "Product",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Blob",
                table: "Image",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProducerModelProducerId",
                table: "Product",
                column: "ProducerModelProducerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Image_ImageId",
                table: "Product",
                column: "ImageId",
                principalTable: "Image",
                principalColumn: "ImageId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Producer_ProducerModelProducerId",
                table: "Product",
                column: "ProducerModelProducerId",
                principalTable: "Producer",
                principalColumn: "ProducerId");
        }
    }
}
