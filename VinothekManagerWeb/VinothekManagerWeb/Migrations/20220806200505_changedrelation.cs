using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VinothekManagerWeb.Migrations
{
    public partial class changedrelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Image_Product_ProductId",
                table: "Image");

            migrationBuilder.DropIndex(
                name: "IX_Image_ProductId",
                table: "Image");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Image");

            migrationBuilder.AlterColumn<double>(
                name: "Alkoholgehalt",
                table: "Product",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "FilePath",
                table: "Image",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ImageId",
                table: "Product",
                column: "ImageId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Image_ImageId",
                table: "Product",
                column: "ImageId",
                principalTable: "Image",
                principalColumn: "ImageId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Image_ImageId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_ImageId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Product");

            migrationBuilder.AlterColumn<double>(
                name: "Alkoholgehalt",
                table: "Product",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FilePath",
                table: "Image",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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
        }
    }
}
