using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.ViewModels
{
    public class BagViewModel
    {
        public List<BagItemViewModel> Items { get; set; }
        public decimal PriceTotal => Items.Sum(x=> x.PriceTotal);
        public string Number { get; set; }
        public Order.Status OrderStatus { get; set; }
    }
}
