using MvcApplication38.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MvcApplication37.Models
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<Sushi> Sushis { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<ViewOrderModel> ViewOrders { get; set; }
        //public Manufacturer Manufacturers { get; set; }
    }
}