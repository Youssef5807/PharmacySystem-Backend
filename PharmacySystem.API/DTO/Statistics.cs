public class TopMedicineDto
{
    public string MedicineName { get; set; }
    public int TotalQuantity { get; set; }
    public decimal TotalSales { get; set; }
}

public class WeeklySalesDto
{
    public string WeekRange { get; set; }
    public decimal TotalAmount { get; set; }
}

public class DashboardStatsDto
{
    public decimal TodaySales { get; set; }
    public decimal WeekSales { get; set; }
    public decimal MonthSales { get; set; }
    public int TotalOrders { get; set; }
    public decimal NetProfit { get; set; }
    public List<WeeklySalesDto> Last4Weeks { get; set; }
}