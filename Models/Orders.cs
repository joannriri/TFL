using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TFL.Models
{
    public class Orders
    {
        [Key]
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        [Precision(16, 2)]
        public decimal OrderTotal { get; set; }        
        public string Email {  get; set; }
        public string PhoneNumber { get; set; }

        //Delivery Method
        public string DeliveryMethod { get; set; }

        //Shipping Details
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ShippingAddress { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        //Payment Method
        public string PaymentMethod { get; set; }
        public string BankSelect { get; set; }
        
        //Shipping Fee
        [Precision(16,2)]
        public decimal ShippingFee { get; set; }

        public virtual ICollection<OrderDetails> OrderDetails { get; set; }

    }
}
