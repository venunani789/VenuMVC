using System;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Venu.Models.Models;
namespace Venu.Models.ViewModels
{
	public class ShopingCartVM
	{
        [ValidateNever]
        public IEnumerable<ShopingCart> ShopingCartList { get; set; }
		public OrderHeader OrderHeader { get; set; }
		//public double OrderTotal { get; set; }
	}
}

