﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication38.Models
{
    public class CartIndexViewModel
    {
        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; }
    }
}