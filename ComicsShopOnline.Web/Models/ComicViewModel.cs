using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;

namespace ComicsShopOnline.Web.Models
{
    public class ComicViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Publisher { get; set; }
        public string CoverURl { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}