using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcApplication38.Models
{
    public class Staff
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Adres { get; set; }
        public string Phone { get; set; }
        public string Dog { get; set; }

    }
}