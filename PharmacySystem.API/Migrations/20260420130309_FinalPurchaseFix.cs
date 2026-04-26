using Microsoft.EntityFrameworkCore.Migrations;

namespace PharmacySystem.API.Migrations
{
    public partial class FinalPurchaseFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orders_Order_ID",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrders_Employees_Employee_ID1",
                table: "PurchaseOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrders_Suppliers_Supplier_ID1",
                table: "PurchaseOrders");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseOrders_Employee_ID1",
                table: "PurchaseOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Employee_ID1",
                table: "PurchaseOrders");

            migrationBuilder.AlterColumn<int>(
                name: "Supplier_ID1",
                table: "PurchaseOrders",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Employee_ID2",
                table: "PurchaseOrders",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Medicine_ID1",
                table: "OrderItems",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "Order_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_Employee_ID",
                table: "PurchaseOrders",
                column: "Employee_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_Employee_ID2",
                table: "PurchaseOrders",
                column: "Employee_ID2");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_Supplier_ID",
                table: "PurchaseOrders",
                column: "Supplier_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Client_ID",
                table: "Orders",
                column: "Client_ID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_Medicine_ID1",
                table: "OrderItems",
                column: "Medicine_ID1");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Medicines_Medicine_ID1",
                table: "OrderItems",
                column: "Medicine_ID1",
                principalTable: "Medicines",
                principalColumn: "Medicine_ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Orders_Order_ID",
                table: "OrderItems",
                column: "Order_ID",
                principalTable: "Orders",
                principalColumn: "Order_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrders_Employees_Employee_ID",
                table: "PurchaseOrders",
                column: "Employee_ID",
                principalTable: "Employees",
                principalColumn: "Employee_ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrders_Employees_Employee_ID2",
                table: "PurchaseOrders",
                column: "Employee_ID2",
                principalTable: "Employees",
                principalColumn: "Employee_ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrders_Suppliers_Supplier_ID",
                table: "PurchaseOrders",
                column: "Supplier_ID",
                principalTable: "Suppliers",
                principalColumn: "Supplier_ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrders_Suppliers_Supplier_ID1",
                table: "PurchaseOrders",
                column: "Supplier_ID1",
                principalTable: "Suppliers",
                principalColumn: "Supplier_ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Medicines_Medicine_ID1",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orders_Order_ID",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrders_Employees_Employee_ID",
                table: "PurchaseOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrders_Employees_Employee_ID2",
                table: "PurchaseOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrders_Suppliers_Supplier_ID",
                table: "PurchaseOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrders_Suppliers_Supplier_ID1",
                table: "PurchaseOrders");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseOrders_Employee_ID",
                table: "PurchaseOrders");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseOrders_Employee_ID2",
                table: "PurchaseOrders");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseOrders_Supplier_ID",
                table: "PurchaseOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_Client_ID",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_Medicine_ID1",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "Employee_ID2",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "Medicine_ID1",
                table: "OrderItems");

            migrationBuilder.AlterColumn<int>(
                name: "Supplier_ID1",
                table: "PurchaseOrders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Employee_ID1",
                table: "PurchaseOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "Client_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_Employee_ID1",
                table: "PurchaseOrders",
                column: "Employee_ID1");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Orders_Order_ID",
                table: "OrderItems",
                column: "Order_ID",
                principalTable: "Orders",
                principalColumn: "Client_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrders_Employees_Employee_ID1",
                table: "PurchaseOrders",
                column: "Employee_ID1",
                principalTable: "Employees",
                principalColumn: "Employee_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrders_Suppliers_Supplier_ID1",
                table: "PurchaseOrders",
                column: "Supplier_ID1",
                principalTable: "Suppliers",
                principalColumn: "Supplier_ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
