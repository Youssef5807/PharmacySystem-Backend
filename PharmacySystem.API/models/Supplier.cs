using System.ComponentModel.DataAnnotations;

namespace PharmacySystem.API.models
{
    public class Supplier
    {
        [Key]
        public int Supplier_ID { get; set; }
        public string Supplier_Name { get; set; }
        public string Supplier_Phone { get; set; }
        public string Supplier_Address { get; set; }

        public ICollection<Purchase_Order> PurchaseOrders { get; set; }
    }

}
