    public class CreatePurchaseOrderDto
{
    public int SupplierId { get; set; }
    public int EmployeeId { get; set; }
    public decimal TotalAmount { get; set; }
}

public class PurchaseOrderResponseDto
{
    public int PurchaseOrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string SupplierName { get; set; }
    public string EmployeeName { get; set; }
}