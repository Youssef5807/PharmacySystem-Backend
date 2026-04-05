namespace PharmacySystem.API.DTO
{
    public class MedicineDtos
    {
    }
}
namespace PharmacySystem.API.DTOs
{
    public class MedicineDto
    {
        public int Medicine_ID { get; set; }
        public string Medicine_Name { get; set; }
        public decimal Selling_Price { get; set; }
        public decimal Cost_Price { get; set; }
        public string Batch_No { get; set; }
        public int Quantity_In_Stock { get; set; }
    }

    public class CreateMedicineDto
    {
        public string Medicine_Name { get; set; }
        public decimal Selling_Price { get; set; }
        public decimal Cost_Price { get; set; }
        public string Batch_No { get; set; }
        public int Quantity_In_Stock { get; set; }
    }

    public class UpdateMedicineDto
    {
        public string Medicine_Name { get; set; }
        public decimal Selling_Price { get; set; }
        public decimal Cost_Price { get; set; }
        public string Batch_No { get; set; }
        public int Quantity_In_Stock { get; set; }
    }
}
