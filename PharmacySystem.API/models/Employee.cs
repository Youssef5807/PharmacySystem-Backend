using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PharmacySystem.API.models
{
    public class Employee
    {
        [Key]
        public int Employee_ID { get; set; }

        public string Employee_Name { get; set; } = string.Empty;
        public string Employee_Role { get; set; } = string.Empty;

        public decimal Salary { get; set; }

        public string Attendance_Details { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        // ✅ Navigation
        public ICollection<Purchase_Order> PurchaseOrders { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
