using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProdCat.Models
{
    [NotMapped]
    public class ProdCatViewModel
    {
        public Product nuProduct { get; set; }
        public Category nuCategory { get; set; }
        public Association nuAssociation { get; set; } = new Association();

        public List<Product> prodViewModel {get; set;}
        public List<Category> catViewModel {get; set;}
        public List<Association> assViewModel {get; set;}



    }
}