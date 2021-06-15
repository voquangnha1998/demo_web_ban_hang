using System;
using System.Collections.Generic;
using System.Web;
using System.Linq;
using Models.EF;

namespace OnlineShop.Models
{
    public class CartItem
        {
        public Product Product { get; set; }
        public int Quantity { get; set; }

    }
}