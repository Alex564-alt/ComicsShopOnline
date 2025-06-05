using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComicsShopOnline.Web.Models
{
    public class CartViewModel
    {
        public List<CartItemViewModel> Items { get; set; } = new List<CartItemViewModel>();
        public decimal GrandTotal => Items.Sum(i => i.TotalPrice);
    }
}