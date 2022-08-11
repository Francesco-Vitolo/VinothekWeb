using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VinothekManagerWeb.Migrations
{
    public partial class moin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventProduct_Event_ProductId",
                table: "EventProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_EventProduct_Product_EventID",
                table: "EventProduct");

            migrationBuilder.AddForeignKey(
                name: "FK_EventProduct_Event_EventID",
                table: "EventProduct",
                column: "EventID",
                principalTable: "Event",
                principalColumn: "EventId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventProduct_Product_ProductId",
                table: "EventProduct",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventProduct_Event_EventID",
                table: "EventProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_EventProduct_Product_ProductId",
                table: "EventProduct");

            migrationBuilder.AddForeignKey(
                name: "FK_EventProduct_Event_ProductId",
                table: "EventProduct",
                column: "ProductId",
                principalTable: "Event",
                principalColumn: "EventId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventProduct_Product_EventID",
                table: "EventProduct",
                column: "EventID",
                principalTable: "Product",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
