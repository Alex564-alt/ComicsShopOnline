using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComicsShopOnline.Web.Models
{
    public class CartItemViewModel
    {
        public int Id { get; set; }
        public int ComicId { get; set; }
        public string ComicName { get; set; }
        public string CoverUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice => Price * Quantity;
    }
}