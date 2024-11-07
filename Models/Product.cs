using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TFL.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public int CategoryID { get; set; }
        [ForeignKey("CategoryID")]
        public virtual ProductCategory ProductCategory { get; set; }
        [Precision(16, 2)]
        public decimal ProductPrice { get; set; }
        public string Description { get; set; }
        public string ImageFile { get; set; }
        public DateTime CreatedAt { get; set; }
        [Required]
        [ScaffoldColumn(false)]
        public int DiscountID { get; set; }
        [ForeignKey("DiscountID")]
        public virtual Discount Discount { get; set; }
        public int Stock { get; set; }
        public int Quantity { get; set; }

        public virtual ICollection<ProductVariants> Variants { get; set; }
    }
}
