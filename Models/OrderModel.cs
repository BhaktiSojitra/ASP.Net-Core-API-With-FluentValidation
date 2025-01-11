namespace API_DEMO.Models
{
    public class OrderModel
    {
        public int OrderID { get; set; }
        public int OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public int CustomerID { get; set; }
        public string? PaymentMode { get; set; }
        public decimal? TotalAmount { get; set; }
        public string ShippingAddress { get; set; }
        public int UserID { get; set; }
    }
    public class CustomerDropDownModel
    {
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
    }
}
