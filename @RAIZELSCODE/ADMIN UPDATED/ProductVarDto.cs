using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TheFabricsLab.Models
{
    public class ProductVarDto
    {
        [Required]
        public int ProductID { get; set; }
        [Required]
        public string Color { get; set; }
        [Required]
        [Precision(16, 2)]
        public decimal Price { get; set; }
        [Required]
        public int Stock { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
