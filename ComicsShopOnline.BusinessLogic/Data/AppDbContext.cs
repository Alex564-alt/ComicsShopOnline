using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ComicsShopOnline.Domain.Entities.User;
using ComicsShopOnline.Domain.Entities.Comic;
using System.Reflection.Emit;
using ComicsShopOnline.Domain.Entities;

namespace ComicsShopOnline.BusinessLogic.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("ComicsShopOnline") 
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }



        //tabele:
        public virtual DbSet<UserDBTable> Users { get; set; }
        public virtual DbSet<ComicDBTable> Comics { get; set; }
        public virtual DbSet<CartItemDBTable> CartItems { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Настройка отношения CartItem -> Comic
            modelBuilder.Entity<CartItemDBTable>()
                .HasRequired(c => c.Comic)
                .WithMany()
                .HasForeignKey(c => c.ComicId)
                .WillCascadeOnDelete(false); // Отключаем каскадное удаление

            // Настройка отношения CartItem -> User
            modelBuilder.Entity<CartItemDBTable>()
                .HasRequired(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}
