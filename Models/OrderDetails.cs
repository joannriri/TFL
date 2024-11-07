using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace TFL.Models
{
    public class OrderDetails
    {
        [Key]
        public int OrderDetailID { get; set; }
        [Required]
        [ScaffoldColumn(false)]
        public int OrderID { get; set; }
        [ForeignKey("OrderID")]
        public virtual Orders Order {  get; set; }
        [Required]
        [ScaffoldColumn(false)]
        public int ProductID { get; set; }
        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }
        public string ProductName { get; set; }
        public string ImageFile { get; set; }
        [ScaffoldColumn(false)]
        public int VariantID { get; set; }
        [ForeignKey("VariantID")]
        public virtual ProductVariants Variants { get; set; }
        public int Quantity { get; set; }
        [Precision(16, 2)]
        public decimal UnitPrice { get; set; }
        [Precision(16,2)]
        public decimal LineTotal => Quantity * UnitPrice;
    }
}
