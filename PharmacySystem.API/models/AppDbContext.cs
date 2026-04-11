using Microsoft.EntityFrameworkCore;
using PharmacySystem.API.models;

namespace PharmacySystem.API.models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

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

            // ضبط مفتاح العميل
            modelBuilder.Entity<Client>().HasKey(c => c.Client_ID);

            // ضبط مفتاح الأوردر والترقيم التلقائي
            modelBuilder.Entity<Order>().HasKey(o => o.Order_ID);
            modelBuilder.Entity<Order>().Property(o => o.Order_ID).ValueGeneratedOnAdd();

            // ربط Order_Item بـ Medicine (منعاً للعمود الإضافي)
            modelBuilder.Entity<Order_Item>()
                .HasOne(oi => oi.Medicine)
                .WithMany(m => m.OrderItems)
                .HasForeignKey(oi => oi.Medicine_ID);

            // ربط Order_Item بـ Order
            modelBuilder.Entity<Order_Item>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.Order_ID);

            // علاقات الموظف والعميل مع الأوردر
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Employee)
                .WithMany(e => e.Orders)
                .HasForeignKey(o => o.Employee_ID);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Client)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.Client_ID);
        }
    }
}