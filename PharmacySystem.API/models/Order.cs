using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacySystem.API.models
{
    public class Order
    {
        [Key] // التعديل: المفتاح الأساسي هنا
        public int Order_ID { get; set; }

        public int Client_ID { get; set; }
        public int Employee_ID { get; set; }

        public DateTime Order_Date { get; set; }
        public decimal Total_Amount { get; set; }
        public string Payment_Method { get; set; }

        // العلاقات
        [ForeignKey("Client_ID")]
        public Client Client { get; set; }

        [ForeignKey("Employee_ID")]
        public Employee Employee { get; set; }

        public ICollection<Order_Item> OrderItems { get; set; }
    }
}