using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VinothekManagerWeb.Migrations
{
    public partial class moin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    ImageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Blob = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.ImageId);
                });

            migrationBuilder.CreateTable(
                name: "Producer",
                columns: table => new
                {
                    ProducerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Region = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producer", x => x.ProducerId);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Art = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qualitätssiegel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rebsorten = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Geschmack = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Alkoholgehalt = table.Column<double>(type: "float", nullable: true),
                    Jahrgang = table.Column<int>(type: "int", nullable: false),
                    Beschreibung = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Preis = table.Column<double>(type: "float", nullable: true),
                    Aktiv = table.Column<bool>(type: "bit", nullable: false),
                    ImageId = table.Column<int>(type: "int", nullable: false),
                    ProducerModelProducerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Product_Image_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Image",
                        principalColumn: "ImageId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Product_Producer_ProducerModelProducerId",
                        column: x => x.ProducerModelProducerId,
                        principalTable: "Producer",
                        principalColumn: "ProducerId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_ImageId",
                table: "Product",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProducerModelProducerId",
                table: "Product",
                column: "ProducerModelProducerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropTable(
                name: "Producer");
        }
    }
}
