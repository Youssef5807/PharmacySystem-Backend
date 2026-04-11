using System.ComponentModel.DataAnnotations; // تأكد من وجود السطر ده

namespace PharmacySystem.API.models
{
    public class Client
    {
        [Key] // ضيف دي فوراً
        public int Client_ID { get; set; }

        public string Client_Name { get; set; } = string.Empty;
        public string Client_Phone { get; set; } = string.Empty;
        public string Client_Address { get; set; } = string.Empty;

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}