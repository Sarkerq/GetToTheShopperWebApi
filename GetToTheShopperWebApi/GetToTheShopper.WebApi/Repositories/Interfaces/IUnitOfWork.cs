using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetToTheShopper.WebApi.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IReceiptRepository Receipts { get; }
        IShopProductRepository ShopProducts { get; }
        IProductRepository Products { get; }
        IShopRepository Shops { get; }
        IReceiptProductRepository ReceiptProduct { get; }
        IUserRepository Users { get; }
        ISharedReceiptRepository SharedReceipts { get; }

        int Save();
    }
}
