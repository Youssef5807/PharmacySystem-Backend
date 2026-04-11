using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization; // ضروري لمنع الـ Loop

namespace PharmacySystem.API.models
{
    public class Order_Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Order_Item_ID { get; set; }

        public int Order_ID { get; set; }

        [ForeignKey("Medicine")]
        public int Medicine_ID { get; set; }

        public int Quantity { get; set; }
        public decimal Sub_Total { get; set; }

        // العلاقات
        [ForeignKey("Order_ID")]
        [JsonIgnore] // التعديل: يمنع الخطأ عند إرجاع البيانات (Serializer Loop)
        public Order Order { get; set; }

        public Medicine Medicine { get; set; }
    }
}