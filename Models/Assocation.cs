using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ProdCat.Models
{

    public class Association
    {
        [Key]
        public int AssociationId { get; set; }

        //foreign keys
        public int ProductId { get; set; }
        public int CategoryId { get; set; }

        //nav props
        public Category Category {get; set;}
        public Product Product {get;set;}
    }
}