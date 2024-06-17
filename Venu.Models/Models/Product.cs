using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Venu.Models.Models
{
    public class Product
    {
        [Key]
        public int id { get; set; }
        [Required]
        [Display(Name = "Title ")]
        public string? Title { get; set; }
        [Required]
        [Display(Name = "Description ")]
        public string? Description { get; set; }
        [Required]
        [Display(Name = "ISBN ")]
        public string? ISBN { get; set; }
        [Required]
        [Display(Name = "Author ")]
        public string? Author { get; set; }

        [Required]
        [Display(Name = "List of Price ")]
        [Range(1, 100)]
        public double ListPrice { get; set; }
        [Required]
        [Display(Name = "price for 1-50 ")]
        [Range(1, 100)]
        public double Price { get; set; }
        [Required]
        [Display(Name = "Price for 50+ ")]
        [Range(1, 100)]
        public double Price50 { get; set; }
        [Required]
        [Display(Name = "Price for 100+ ")]
        [Range(1, 100)]
        public double Price100 { get; set; }
        [ValidateNever]
        public string? ImageUrl { get; set; }
        public int CatogoryId { get; set; }
        [ForeignKey("CatogoryId")]
        [ValidateNever]
        public Catogory Catogory { get; set; }

    }
}

