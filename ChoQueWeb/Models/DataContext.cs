using ChoQueVN.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChoQueVN.Models
{
    public class DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=HOANGUYEN;database=choqueVN.Com;UID=sa;Password=Ta0lacuamay1;");
        }

        //khai bao su dung class o day
        public DbSet<User> Users { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Statistical> Statisticals { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<CategoryNews> CategoryNews { get; set; }
        public DbSet<Bill> Bills { get; set; }
    }
}
