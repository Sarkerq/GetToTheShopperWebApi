using GetToTheShopper.WebApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace GetToTheShopper.WebApi.Models
{
    public class GetToTheShopperContext : IdentityDbContext<ApplicationUser>
    {
        public GetToTheShopperContext(DbContextOptions<GetToTheShopperContext> options)
            : base(options)
        { }
        public GetToTheShopperContext()
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<ReceiptProduct> ReceiptProducts { get; set; }
        public DbSet<ShopProduct> ShopProducts { get; set; }
        public DbSet<SharedReceipt> SharedReceipts { get; set; }

    }
}
