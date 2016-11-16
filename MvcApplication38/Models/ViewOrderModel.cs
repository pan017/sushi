using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcApplication38.Models
{
    public class ViewOrderModel
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Adres { get; set; }
        public string Phone { get; set; }
        public string Order { get; set; }
        public double Cost { get; set; }
        public string Staff { get; set; }
        public DateTime Date { get; set; }
        public bool State { get; set; }
    }
}