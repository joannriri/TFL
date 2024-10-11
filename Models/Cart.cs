using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TFL.Models
{
    public class Cart
    {
        [Key]
        public int CartID { get; set; }
        [Required]
        public int ProductID { get; set; }
        [ForeignKey("ProductID")]
        public virtual Product Products { get; set; }

        public string ProductName { get; set; }
        public int Quantity { get; set; }
        [Precision(16,2)]
        public decimal Price { get; set; }
        [Precision(16,2)]
        public decimal Total {
            get { return Quantity * Price; }
        
        }
        public string Image { get; set; }
        public Cart()
        {

        }
        public Cart(Product product)
        {
            ProductID = product.ProductID;
            ProductName = product.ProductName;
            Price = product.ProductPrice;
            Quantity = 1;
            Image = product.ImageFile;

        }
        
        

    }
}
