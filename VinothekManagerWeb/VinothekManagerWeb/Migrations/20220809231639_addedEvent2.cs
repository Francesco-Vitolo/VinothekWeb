using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VinothekManagerWeb.Migrations
{
    public partial class addedEvent2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventModelProductModel_EventModel_EventsProductId",
                table: "EventModelProductModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventModel",
                table: "EventModel");

            migrationBuilder.RenameTable(
                name: "EventModel",
                newName: "Event");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Event",
                table: "Event",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventModelProductModel_Event_EventsProductId",
                table: "EventModelProductModel",
                column: "EventsProductId",
                principalTable: "Event",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventModelProductModel_Event_EventsProductId",
                table: "EventModelProductModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Event",
                table: "Event");

            migrationBuilder.RenameTable(
                name: "Event",
                newName: "EventModel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventModel",
                table: "EventModel",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventModelProductModel_EventModel_EventsProductId",
                table: "EventModelProductModel",
                column: "EventsProductId",
                principalTable: "EventModel",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
