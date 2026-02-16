using System.ComponentModel.DataAnnotations;

namespace PharmacySystem.API.models
{
    public class Purchase_Order
    {
        [Key]
        public int PO_ID { get; set; }
        public int Supplier_ID { get; set; }
        public int Employee_ID { get; set; }
        public DateTime PO_Date { get; set; }
        public decimal Total_Amount { get; set; }

        public Supplier Supplier { get; set; }
        public Employee Employee { get; set; }

        public ICollection<Purchase_Item> PurchaseItems { get; set; }
    }

}
