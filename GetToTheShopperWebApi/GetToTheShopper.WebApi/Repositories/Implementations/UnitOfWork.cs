using GetToTheShopper.WebApi.Models;
using GetToTheShopper.WebApi.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetToTheShopper.WebApi.Repositories.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {

        public readonly GetToTheShopperContext _context;

        public UnitOfWork(GetToTheShopperContext context)
        {
            _context = context;

            Users = new UserRepository(_context);
            Receipts = new ReceiptRepository(_context);
            ShopProducts = new ShopProductRepository(_context);
            Products = new ProductRepository(_context);
            Shops = new ShopRepository(_context);
            ReceiptProduct = new ReceiptProductRepository(_context);
            SharedReceipts = new SharedReceiptRepository(_context);
        }

        public IReceiptRepository Receipts { get; private set; }
        public IShopProductRepository ShopProducts { get; private set; }
        public IProductRepository Products { get; private set; }
        public IShopRepository Shops { get; private set; }
        public IReceiptProductRepository ReceiptProduct { get; private set; }
        public IUserRepository Users { get; private set; }
        public ISharedReceiptRepository SharedReceipts { get; private set; }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            // We trust that .Net Core does that for us
            //_context.Dispose();
        }
    }
}
