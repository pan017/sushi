using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcApplication38.Models
{
    public class Sushi
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int SushiId { get; set; }
        public string Name { get; set; }
        public string Composition { get; set; }
        public string Colorie { get; set; }
        public string Type { get; set; }
        public string Photo { get; set; }
        public double Price { get; set; }
    }
    
}