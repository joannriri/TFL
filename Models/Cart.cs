using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

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
            [Precision(16, 2)]
            public decimal Price { get; set; }
            [Precision(16, 2)]
            public decimal Total
            {
                get { return Quantity * Price; }

            }
            public string Image { get; set; }
            [Required]
            [ScaffoldColumn(false)]
            public int VariantID { get; set; }
            [ForeignKey("VariantID")]
            public virtual ProductVariants Variants { get; set; }
            
         

            


        
    }
}
