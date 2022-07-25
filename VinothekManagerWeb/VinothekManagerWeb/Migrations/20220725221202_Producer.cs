using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VinothekManagerWeb.Migrations
{
    public partial class Producer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProducerModelId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Producer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Region = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producer", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProducerModelId",
                table: "Products",
                column: "ProducerModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Producer_ProducerModelId",
                table: "Products",
                column: "ProducerModelId",
                principalTable: "Producer",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Producer_ProducerModelId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Producer");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProducerModelId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProducerModelId",
                table: "Products");
        }
    }
}
