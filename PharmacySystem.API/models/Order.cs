using System.ComponentModel.DataAnnotations;

namespace PharmacySystem.API.models
{
    public class Order
    {
        [Key]
        public int Order_ID { get; set; }
        public int Client_ID { get; set; }
        public int Employee_ID { get; set; }
        public DateTime Order_Date { get; set; }
        public decimal Total_Amount { get; set; }
        public string Payment_Method { get; set; }

        public Client Client { get; set; }
        public Employee Employee { get; set; }

        public ICollection<Order_Item> OrderItems { get; set; }
    }
}
