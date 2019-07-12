using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace productsNcategoriescSharp.Models
{
    public class Product
    {
        [Key]
        public int ProductId {get;set;}

        public string Name {get;set;}

        public string Description {get;set;}

        public double Price {get;set;}

        public List<Association> Associations {get;set;}

        public DateTime CreatedAt {get;set;}

        public DateTime UpdateddAt {get;set;}
    }
}