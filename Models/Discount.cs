using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TFL.Models
{
    public class Discount
    {
        [Key]
        public int DiscountID { get; set; }
        public string Name { get; set; }
        [Precision(16, 2)]
        public decimal DiscountPercent { get; set; }

        public Boolean Active { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
