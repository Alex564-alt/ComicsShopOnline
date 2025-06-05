using ComicsShopOnline.Domain.Entities.Comic;
using ComicsShopOnline.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicsShopOnline.Domain.Entities
{
    public class CartItemDBTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int ComicId { get; set; }
        public int Quantity { get; set; }
        public int UserId { get; set; }

        [ForeignKey("ComicId")]
        public virtual ComicDBTable Comic { get; set; }

        [ForeignKey("UserId")]
        public virtual UserDBTable User { get; set; }
    }
}
