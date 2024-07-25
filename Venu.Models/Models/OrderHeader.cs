﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Venu.Models.Models
{
	public class OrderHeader
	{
		[Key]
		public int id { get; set; }
		public string? ApplicationUserId { get; set; }
		[ForeignKey("ApplicationUserId")]
		[ValidateNever]
		public ApplicationUser ApplicationUser { get; set; }
		public DateTime OrderDate { get; set; }
		public DateTime MyProperty { get; set; }
		public double OrderTotal { get; set; }
		public string? OrderStatus { get; set; }
		public string? PaymentStatus { get; set; }
		public string? TrackingNumber { get; set; }
		public string? Carrier { get; set; }
		public DateTime PaymentDate { get; set; }
		public DateOnly PaymentDueDate { get; set; }
		//add migration for session id
		public string? SessionId { get; set; }
		public string? PayementIntentId { get; set; }
		[Required]
        public string? Name { get; set; }
        [Required]
        public string? StreetAddress { get; set; }
        [Required]
        public string? City { get; set; }
        [Required]
        public string? State { get; set; }
        [Required]
        public string? PostalCode { get; set; }
        [Required]
        public string? PhoneNumber { get; set; }

    }
}

