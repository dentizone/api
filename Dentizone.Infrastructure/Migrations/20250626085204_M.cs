using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dentizone.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class M : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShipmentActivities_Orders_OrderId",
                table: "ShipmentActivities");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "ShipmentActivities",
                newName: "ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_ShipmentActivities_OrderId",
                table: "ShipmentActivities",
                newName: "IX_ShipmentActivities_ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShipmentActivities_OrderItems_ItemId",
                table: "ShipmentActivities",
                column: "ItemId",
                principalTable: "OrderItems",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShipmentActivities_OrderItems_ItemId",
                table: "ShipmentActivities");

            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "ShipmentActivities",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_ShipmentActivities_ItemId",
                table: "ShipmentActivities",
                newName: "IX_ShipmentActivities_OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShipmentActivities_Orders_OrderId",
                table: "ShipmentActivities",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}
