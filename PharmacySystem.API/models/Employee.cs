using System.ComponentModel.DataAnnotations;
using PharmacySystem.API.models;

public class Employee
{
    [Key]
    public Guid Employee_ID { get; set; } = Guid.NewGuid();

    [Required]
    [MaxLength(70)]
    public string Employee_Name { get; set; }

    [Required]
    public string Employee_Role { get; set; }

    public decimal Salary { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string PasswordHash { get; set; }

    public string Attendance_Details { get; set; }

    public ICollection<Order> Orders { get; set; }
    public ICollection<Purchase_Order> PurchaseOrders { get; set; }
}
