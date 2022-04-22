using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
namespace ProdCat.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string CatName { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        //foreign keys

        //relationships
        public List<Association> Products { get; set; }

    }
}