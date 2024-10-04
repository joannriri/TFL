﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TFL.Models
{
    public class Product
    {
        [Key] public int ProductID { get; set; }
        public string ProductName { get; set; }
        [Required]
        public int CategoryID { get; set; }
        [ForeignKey("CategoryID")]
        public virtual ProductCategory ProductCategory { get; set; }
        [Precision(16,2)]
        public decimal ProductPrice { get; set; }
        public string Description { get; set; }
        public string ImageFile { get; set; }
        public DateTime CreatedAt { get; set; }
        [Required]
        [ScaffoldColumn(false)]
        public int DiscountID { get; set; }
        [ForeignKey("DiscountID")]
        public virtual Discount Discount { get; set; }



    }
}
