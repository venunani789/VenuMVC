using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Venu.Models.Models
{
	public class OrderDetail
	{
		[Required]
		public int id { get; set; }
		[Required]
		public int OrderHearderId { get; set; }
		[ForeignKey("OrderHearderId")]
		[ValidateNever]
		public OrderHeader OrderHeader { get; set; }

		[Required]
		public int? ProductId { get; set; }
		[ForeignKey("ProductId")]
		[ValidateNever]
		public Product Product { get; set; }

		public int Count { get; set; }
		public double Price { get; set; }
	}
}

