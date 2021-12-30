﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IBasketService
    {
        Task EmptyBasketAsync(string buyerId);
        Task RemoveBasketItemAsync(string buyerId, int basketItemId);
    }
}
