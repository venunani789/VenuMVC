using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace Venu.Models.Models
{
	public class Catogory
	{

        [Key]
        
        public int id { get;set; }


        [Required]
        [DisplayName("Catagory Name")]

        [MaxLength(30)]
        public string? Name { get; set; }

        
        [DisplayName("Catogory Order")]
        [Range(1,100,ErrorMessage ="Please Mama Enter valid number which is between 1-100")]
        public int DisplayOrder { get; set; }

    }
}


