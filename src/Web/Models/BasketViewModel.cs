using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class BasketViewModel
    {
        public List<BasketItemViewModel> Items { get; set; }

        public decimal TotalPrice { get; set; }

        public string TotalPriceUsd => TotalPrice.ToString("c2", new CultureInfo("en-US"));
    }
}

