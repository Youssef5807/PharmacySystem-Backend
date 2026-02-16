using Microsoft.EntityFrameworkCore;
using PharmacySystem.API.models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Client> Clients { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Medicine> Medicines { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Order_Item> OrderItems { get; set; }
    public DbSet<Purchase_Order> PurchaseOrders { get; set; }
    public DbSet<Purchase_Item> PurchaseItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Client>().HasData(
            new Client { Client_ID = Guid.NewGuid(), Client_Name = "Youssef Maher", Client_Phone = "0100000000", Client_Address = "Cairo" },
            new Client { Client_ID = Guid.NewGuid(), Client_Name = "Youssef Maher", Client_Phone = "0100050000", Client_Address = "Cairo" },
            new Client { Client_ID = Guid.NewGuid(),Client_Name = "Ahmed Ali", Client_Phone = "0101111111", Client_Address = "Giza" }
        );

        modelBuilder.Entity<Employee>().HasData(
            new Employee { Employee_ID = Guid.NewGuid(), Employee_Name = "Mona Samir", Employee_Role = "Pharmacist", Salary = 5000, Attendance_Details = "Full-time" , Email = "admin@pharmacy.moh.com" , PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123")},
                        new Employee { Employee_ID = Guid.NewGuid(), Employee_Name = "System Admin", Employee_Role = "Admin", Salary = 5000, Attendance_Details = "Full-time", Email = "RealAdmin@pharmacy.moh.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword("Real123") }

        );

        modelBuilder.Entity<Medicine>().HasData(
            new Medicine { Medicine_ID = 1, Medicine_Name = "Paracetamol", Selling_Price = 10, Cost_Price = 5, Batch_No = "B123", Quantity_In_Stock = 100 }
        );
    }
}
