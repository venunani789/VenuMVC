using System;

using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Venu.Models.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Venu.Models.ViewModels
{
	public class ProductVM
	{
        public Product? Product { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem>? CatogoryList { get; set; }
    }
}

