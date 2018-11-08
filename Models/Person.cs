using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Tel { get; set; }
        public List<Order> Orders { get; set; }

        public Person() { }
    }
}