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

            // مفاتيح الجداول الأساسية
            modelBuilder.Entity<Client>().HasKey(c => c.Client_ID);
            modelBuilder.Entity<Order>().HasKey(o => o.Order_ID);
            modelBuilder.Entity<Purchase_Order>().HasKey(po => po.PO_ID);

            // علاقات الـ Order_Item
            modelBuilder.Entity<Order_Item>()
                .HasOne(oi => oi.Medicine)
                .WithMany() // لو عامل لستة في Medicine ضيف اسمها هنا
                .HasForeignKey(oi => oi.Medicine_ID);

            modelBuilder.Entity<Order_Item>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.Order_ID);

            // --- التعديل الجديد لعلاقات الـ Purchase_Order ---
            modelBuilder.Entity<Purchase_Order>()
                .HasOne(po => po.Supplier)
                .WithMany() // بيفهم إن المورد عنده أوامر شراء كتير
                .HasForeignKey(po => po.Supplier_ID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Purchase_Order>()
                .HasOne(po => po.Employee)
                .WithMany()
                .HasForeignKey(po => po.Employee_ID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}