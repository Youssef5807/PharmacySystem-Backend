public class CreateOrderDto
{
    public int Client_ID { get; set; }
    public int Employee_ID { get; set; }

    public List<OrderItemDto> Items { get; set; } = new List<OrderItemDto>();
}
