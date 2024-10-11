using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TFL.Models
{
    public class Orders
    {
        [Key]
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        [Precision(16,2)]
        public decimal OrderTotal { get; set; }
        [Required]
        [ScaffoldColumn(false)]
        public int OrderDetailID { get; set; }
        [ForeignKey("OrderDetailID")]
        public virtual OrderDetails OrderDetails { get; set; }
    }
}
