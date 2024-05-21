using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Venu.Models.Models
{
	public class Product
	{
		[Key]
		public int id { get; set; }
		[Required]
		public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
		public string ISBN { get; set; }
		[Required]
		public string Author { get; set; }

		[Required]
		[Display(Name="List of Price ")]
       [Range(1,100)]
        public double ListPrice { get; set; }
        [Required]
        [Display(Name = "price for 1-50 ")]
        [Range(1, 100)]
        public double Price { get; set; }
        [Required]
        [Display(Name = "Price for 50+ ")]
        [Range(1, 100)]
        public double Price50{ get; set; }
        [Required]
        [Display(Name = "Price for 100+ ")]
        [Range(1, 100)]
        public double Price100 { get; set; }
    }
}

