using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TFL.Models
{
    public class OrderDetails
    {
        [Key]
        public int OrderDetailID { get; set; }
        [Required]
        public int ProductID { get; set; }
        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }
        public int Quantity { get; set; }
        [Precision(16,2)]
        public decimal UnitPrice { get; set; }
    }
}
