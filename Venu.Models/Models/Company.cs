using System;
using System.ComponentModel.DataAnnotations;

namespace Venu.Models.Models
{
	public class Company
	{
        [Key]
		public int id { get; set; }
        [Required]
		public string? Name { get; set; }
        public string? StreetAddress { get; set; }
        public string? City { get; set; }
        public int State { get; set; }
        public string? PostalCode { get; set; }
        public string? PhoneNumber { get; set; }


    }
}

