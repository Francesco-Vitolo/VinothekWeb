using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VinothekManagerWeb.Migrations
{
    public partial class eventid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventModelProductModel_Event_EventsProductId",
                table: "EventModelProductModel");

            migrationBuilder.RenameColumn(
                name: "EventsProductId",
                table: "EventModelProductModel",
                newName: "EventsEventId");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Event",
                newName: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventModelProductModel_Event_EventsEventId",
                table: "EventModelProductModel",
                column: "EventsEventId",
                principalTable: "Event",
                principalColumn: "EventId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventModelProductModel_Event_EventsEventId",
                table: "EventModelProductModel");

            migrationBuilder.RenameColumn(
                name: "EventsEventId",
                table: "EventModelProductModel",
                newName: "EventsProductId");

            migrationBuilder.RenameColumn(
                name: "EventId",
                table: "Event",
                newName: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventModelProductModel_Event_EventsProductId",
                table: "EventModelProductModel",
                column: "EventsProductId",
                principalTable: "Event",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
