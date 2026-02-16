using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PharmacySystem.API.models
{
    public class Medicine
    {
        [Key]
        public int Medicine_ID { get; set; }

        [Required]
        [MaxLength(100)]
        public string Medicine_Name { get; set; }

        public decimal Selling_Price { get; set; }
        public decimal Cost_Price { get; set; }

        [MaxLength(50)]
        public string Batch_No { get; set; }

        public int Quantity_In_Stock { get; set; }

        public DateTime Expiry_Date { get; set; }

        public ICollection<Order_Item> OrderItems { get; set; } = new List<Order_Item>();
        public ICollection<Purchase_Item> PurchaseItems { get; set; } = new List<Purchase_Item>();
    }
}
