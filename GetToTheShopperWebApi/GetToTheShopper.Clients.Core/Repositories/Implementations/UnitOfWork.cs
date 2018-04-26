using ShoppingList.Core.Model;
using ShoppingList.Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.Core.Repositories.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly GetToTheShopperContext _context;

        public UnitOfWork()
        {
            _context = new GetToTheShopperContext();
            Receipts = new ReceiptRepository(_context);
            ShopProducts = new ShopProductRepository(_context);
            Products = new ProductRepository(_context);
            Shops = new ShopRepository(_context);
            ReceiptProduct = new ReceiptProductRepository(_context);
        }

        public IReceiptRepository Receipts { get; private set; }
        public IShopProductRepository ShopProducts { get; private set; }
        public IProductRepository Products { get; private set; }
        public IShopRepository Shops { get; private set; }

        public IReceiptProductRepository ReceiptProduct { get; private set; }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
