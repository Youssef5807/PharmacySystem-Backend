using System.ComponentModel.DataAnnotations;

namespace PharmacySystem.API.models
{
    public class Client
    {
        [Key]
        public Guid Client_ID { get; set; } = Guid.NewGuid();
        public string Client_Name { get; set; }
        public string Client_Phone { get; set; }
        public string Client_Address { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
