using System.ComponentModel.DataAnnotations;

namespace PharmacySystem.API.models
{
    public class Order_Item
    {
        [Key]
        public int Order_Item_ID { get; set; }
        public int Order_ID { get; set; }
        public int Medicine_ID { get; set; }
        public int Quantity_Sold { get; set; }
        public decimal Sub_Total { get; set; }

        public Order Order { get; set; }
        public Medicine Medicine { get; set; }
    }

}
