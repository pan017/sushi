using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcApplication38.Models
{
    public class Order
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }
        public virtual Sushi Product { get; set; }
        public virtual Client Client { get; set; }
        public virtual Staff Staff { get; set; }

        public DateTime Date { get; set; }
        public bool State { get; set; }
    }
}