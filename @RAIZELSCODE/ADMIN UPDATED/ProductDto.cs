using System.ComponentModel.DataAnnotations;

namespace TheFabricsLab.Models
{
    public class ProductDto
    {
        [Required]
        public string ProductName { get; set; }
        [Required]
        public int CategoryID { get; set; }
        [Required]
        public decimal ProductPrice { get; set; }
        [Required]
        public string Description { get; set; }
        public IFormFile? ImageFile { get; set; }
        [Required]
        public int DiscountID { get; set; }
        [Required]
        public int Stock { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
