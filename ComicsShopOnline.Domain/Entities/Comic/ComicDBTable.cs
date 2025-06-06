﻿using ComicsShopOnline.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicsShopOnline.Domain.Entities.Comic
{
    public class ComicDBTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }
        public Publisher Publisher { get; set; }
        public string CoverURl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
