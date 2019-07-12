using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace productsNcategoriescSharp.Models
{
    public class Category
    {
        [Key]
        public int CategoryId {get;set;}

        public string Name {get;set;}

        public List<Association> Associations {get;set;}

        public DateTime CreatedAt {get;set;}

        public DateTime UpdateddAt {get;set;}
    }
}