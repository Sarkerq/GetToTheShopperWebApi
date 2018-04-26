﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.Core.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IReceiptRepository Receipts { get; }
        IReceiptProductRepository ReceiptProduct { get; }
        int Save();
    }
}
