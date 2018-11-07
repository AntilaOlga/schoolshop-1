using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Models
{
    public class Order
    {
        public enum Status
        {
            Bag = 1,
            Confirmed = 2,
            Paid = 3,
            Received = 4,
            Canceled = 0
        }

        public Order()
        {
            Number = GenerateNumber();
            Items = new List<OrderItem>();
            OrderStatus = Status.Bag;
        }

        public int Id { get; set; }
        public string Number { get; set; }
        public List<OrderItem> Items { get; set; }
        public Status OrderStatus { get; set; } 

        private string GenerateNumber()
        {
            return $"{DateTime.Now.ToString("yyyyMMdd-HHmmss")}";
        }


    }
}