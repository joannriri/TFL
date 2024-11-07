using TFL.Models;

namespace TFL.ViewModels
{
    public class CheckoutView
    {
        public List<Cart> CartItems { get; set; } 
        public decimal TotalAmount { get; set; }


        //Order form
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string DeliveryMethod { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string ShippingAddress { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string PaymentMethod { get; set; } = string.Empty;
        public string BankSelect { get; set; } = string.Empty;

        public decimal ShippingFee { get; set; } = decimal.Zero;
    }
}
