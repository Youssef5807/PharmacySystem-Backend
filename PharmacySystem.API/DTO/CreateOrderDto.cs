public class CreateOrderDto
{
    public int Client_ID { get; set; }
    public int Employee_ID { get; set; }

    public List<OrderItemDto> Items { get; set; } = new List<OrderItemDto>();
}
public class OrderResponseDto
{
    public int OrderId { get; set; }
    public string ClientName { get; set; }
    public string EmployeeName { get; set; }
    public int EmployeeId { get; set; }  
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string PaymentMethod { get; set; }
    public List<OrderItemResponseDto> Items { get; set; }
}

public class OrderItemResponseDto
{
    public string MedicineName { get; set; }
    public int Quantity { get; set; }
    public decimal SubTotal { get; set; }
}
