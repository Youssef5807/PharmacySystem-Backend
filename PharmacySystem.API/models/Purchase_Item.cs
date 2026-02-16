using System.ComponentModel.DataAnnotations;

namespace PharmacySystem.API.models
{
    public class Purchase_Item
    {
        [Key]
        public int Purchase_Item_ID { get; set; }
        public int PO_ID { get; set; }
        public int Medicine_ID { get; set; }
        public int Quantity_Bought { get; set; }
        public decimal Unit_Cost { get; set; }
        public DateTime Expiry_Date { get; set; }

        public Purchase_Order PurchaseOrder { get; set; }
        public Medicine Medicine { get; set; }
    }

}
