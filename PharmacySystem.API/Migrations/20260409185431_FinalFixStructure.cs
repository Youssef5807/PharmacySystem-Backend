using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PharmacySystem.API.Migrations
{
    public partial class FinalFixStructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Client_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Client_Name = table.Column<string>(nullable: false),
                    Client_Phone = table.Column<string>(nullable: false),
                    Client_Address = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Client_ID);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Employee_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Employee_Name = table.Column<string>(nullable: false),
                    Employee_Role = table.Column<string>(nullable: false),
                    Salary = table.Column<decimal>(nullable: false),
                    Attendance_Details = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Employee_ID);
                });

            migrationBuilder.CreateTable(
                name: "Medicines",
                columns: table => new
                {
                    Medicine_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Medicine_Name = table.Column<string>(maxLength: 100, nullable: false),
                    Selling_Price = table.Column<decimal>(nullable: false),
                    Cost_Price = table.Column<decimal>(nullable: false),
                    Batch_No = table.Column<string>(maxLength: 50, nullable: false),
                    Quantity_In_Stock = table.Column<int>(nullable: false),
                    Expiry_Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicines", x => x.Medicine_ID);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Supplier_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Supplier_Name = table.Column<string>(nullable: false),
                    Supplier_Phone = table.Column<string>(nullable: false),
                    Supplier_Address = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Supplier_ID);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Client_ID = table.Column<int>(nullable: false),
                    Order_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Employee_ID = table.Column<int>(nullable: false),
                    Order_Date = table.Column<DateTime>(nullable: false),
                    Total_Amount = table.Column<decimal>(nullable: false),
                    Payment_Method = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Client_ID);
                    table.ForeignKey(
                        name: "FK_Orders_Clients_Client_ID",
                        column: x => x.Client_ID,
                        principalTable: "Clients",
                        principalColumn: "Client_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Employees_Employee_ID",
                        column: x => x.Employee_ID,
                        principalTable: "Employees",
                        principalColumn: "Employee_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrders",
                columns: table => new
                {
                    PO_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Supplier_ID = table.Column<int>(nullable: false),
                    Employee_ID = table.Column<int>(nullable: false),
                    PO_Date = table.Column<DateTime>(nullable: false),
                    Total_Amount = table.Column<decimal>(nullable: false),
                    Supplier_ID1 = table.Column<int>(nullable: false),
                    Employee_ID1 = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrders", x => x.PO_ID);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_Employees_Employee_ID1",
                        column: x => x.Employee_ID1,
                        principalTable: "Employees",
                        principalColumn: "Employee_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_Suppliers_Supplier_ID1",
                        column: x => x.Supplier_ID1,
                        principalTable: "Suppliers",
                        principalColumn: "Supplier_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Order_Item_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Order_ID = table.Column<int>(nullable: false),
                    Medicine_ID = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Sub_Total = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Order_Item_ID);
                    table.ForeignKey(
                        name: "FK_OrderItems_Medicines_Medicine_ID",
                        column: x => x.Medicine_ID,
                        principalTable: "Medicines",
                        principalColumn: "Medicine_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_Order_ID",
                        column: x => x.Order_ID,
                        principalTable: "Orders",
                        principalColumn: "Client_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseItems",
                columns: table => new
                {
                    Purchase_Item_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PO_ID = table.Column<int>(nullable: false),
                    Medicine_ID = table.Column<int>(nullable: false),
                    Quantity_Bought = table.Column<int>(nullable: false),
                    Unit_Cost = table.Column<decimal>(nullable: false),
                    Expiry_Date = table.Column<DateTime>(nullable: false),
                    PurchaseOrderPO_ID = table.Column<int>(nullable: false),
                    Medicine_ID1 = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseItems", x => x.Purchase_Item_ID);
                    table.ForeignKey(
                        name: "FK_PurchaseItems_Medicines_Medicine_ID1",
                        column: x => x.Medicine_ID1,
                        principalTable: "Medicines",
                        principalColumn: "Medicine_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseItems_PurchaseOrders_PurchaseOrderPO_ID",
                        column: x => x.PurchaseOrderPO_ID,
                        principalTable: "PurchaseOrders",
                        principalColumn: "PO_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_Medicine_ID",
                table: "OrderItems",
                column: "Medicine_ID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_Order_ID",
                table: "OrderItems",
                column: "Order_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Employee_ID",
                table: "Orders",
                column: "Employee_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseItems_Medicine_ID1",
                table: "PurchaseItems",
                column: "Medicine_ID1");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseItems_PurchaseOrderPO_ID",
                table: "PurchaseItems",
                column: "PurchaseOrderPO_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_Employee_ID1",
                table: "PurchaseOrders",
                column: "Employee_ID1");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_Supplier_ID1",
                table: "PurchaseOrders",
                column: "Supplier_ID1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "PurchaseItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Medicines");

            migrationBuilder.DropTable(
                name: "PurchaseOrders");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Suppliers");
        }
    }
}
