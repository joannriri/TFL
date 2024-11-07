namespace TFL.ViewModels
{
    public class OrderSummaryViewModel
    {
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal OrderTotal { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string DeliveryMethod { get; set; }
        public string ShippingAddress { get; set; }
        public decimal ShippingFee { get; set; }
        public List<OrderItemViewModel> OrderDetails { get; set; }
    }

    public class OrderItemViewModel
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ImageFile { get; set; }
        public string Color { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal { get; set; }
    }
}
