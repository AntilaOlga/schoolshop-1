using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.ViewModels
{
    public class BagItemViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }

        public decimal PriceTotal => Price * Count;
    }
}
