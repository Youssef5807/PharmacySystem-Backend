using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PharmacySystem.API.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Client_ID = table.Column<Guid>(nullable: false),
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
                    Employee_ID = table.Column<Guid>(nullable: false),
                    Employee_Name = table.Column<string>(maxLength: 70, nullable: false),
                    Employee_Role = table.Column<string>(nullable: false),
                    Salary = table.Column<decimal>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: false),
                    Attendance_Details = table.Column<string>(nullable: false)
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
                    Order_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Client_ID = table.Column<int>(nullable: false),
                    Employee_ID = table.Column<int>(nullable: false),
                    Order_Date = table.Column<DateTime>(nullable: false),
                    Total_Amount = table.Column<decimal>(nullable: false),
                    Payment_Method = table.Column<string>(nullable: false),
                    Client_ID1 = table.Column<Guid>(nullable: false),
                    Employee_ID1 = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Order_ID);
                    table.ForeignKey(
                        name: "FK_Orders_Clients_Client_ID1",
                        column: x => x.Client_ID1,
                        principalTable: "Clients",
                        principalColumn: "Client_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Employees_Employee_ID1",
                        column: x => x.Employee_ID1,
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
                    Employee_ID1 = table.Column<Guid>(nullable: false)
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
                    Quantity_Sold = table.Column<int>(nullable: false),
                    Sub_Total = table.Column<decimal>(nullable: false),
                    Order_ID1 = table.Column<int>(nullable: false),
                    Medicine_ID1 = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Order_Item_ID);
                    table.ForeignKey(
                        name: "FK_OrderItems_Medicines_Medicine_ID1",
                        column: x => x.Medicine_ID1,
                        principalTable: "Medicines",
                        principalColumn: "Medicine_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_Order_ID1",
                        column: x => x.Order_ID1,
                        principalTable: "Orders",
                        principalColumn: "Order_ID",
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

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Client_ID", "Client_Address", "Client_Name", "Client_Phone" },
                values: new object[,]
                {
                    { new Guid("50d1889f-2f61-4248-ae13-f39fbb6c90c6"), "Cairo", "Youssef Maher", "0100000000" },
                    { new Guid("33dff5f2-14cf-4e79-947b-595b4caeb429"), "Cairo", "Youssef Maher", "0100050000" },
                    { new Guid("6f0d4712-c186-40bd-9b79-6140402383fe"), "Giza", "Ahmed Ali", "0101111111" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Employee_ID", "Attendance_Details", "Email", "Employee_Name", "Employee_Role", "PasswordHash", "Salary" },
                values: new object[,]
                {
                    { new Guid("3eb3958c-f9fc-4b2e-abbb-89c1436b3852"), "Full-time", "admin@pharmacy.moh.com", "Mona Samir", "Pharmacist", "$2a$11$VlY1FV2ef1CGxJoT6aqmoupVd6zmSULlMG5gtjbItIzkAjllHR2gO", 5000m },
                    { new Guid("6af2a4d0-2e74-4f25-829a-650509d11cb7"), "Full-time", "RealAdmin@pharmacy.moh.com", "System Admin", "Admin", "$2a$11$FgmVMXWUC0Ni/UAVYoVJmu75zO4AsY/xZH1igpXCIoN89nzfvnwVa", 5000m }
                });

            migrationBuilder.InsertData(
                table: "Medicines",
                columns: new[] { "Medicine_ID", "Batch_No", "Cost_Price", "Expiry_Date", "Medicine_Name", "Quantity_In_Stock", "Selling_Price" },
                values: new object[] { 1, "B123", 5m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Paracetamol", 100, 10m });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_Medicine_ID1",
                table: "OrderItems",
                column: "Medicine_ID1");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_Order_ID1",
                table: "OrderItems",
                column: "Order_ID1");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Client_ID1",
                table: "Orders",
                column: "Client_ID1");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Employee_ID1",
                table: "Orders",
                column: "Employee_ID1");

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
