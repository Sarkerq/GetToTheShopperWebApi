﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GetToTheShopper.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using GetToTheShopper.WebApi.Repositories.Interfaces;

namespace GetToTheShopper.WebApi.Repositories.Implementations
{
    public class ShopRepository : Repository<Shop>, IShopRepository
    {
        public ShopRepository(DbContext context) : base(context)
        {
        }

        public GetToTheShopperContext SContext { get => Context as GetToTheShopperContext; }
    }
}
