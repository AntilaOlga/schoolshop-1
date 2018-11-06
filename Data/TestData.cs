using Shop.Data;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Data
{
    public class TestData
    {
        public static void CreateTestData(ShopContext _db)
        {
            var items = _db.Products.ToList();
            if (items.Count() == 0) CreateTestProducts(_db);
        }

        private static void CreateTestProducts(ShopContext _db)
        {
            List<Product> ProductsList = new List<Product>
            {
                new Product("Счастье", 5500),
                new Product("Удача", 3000),
                new Product("Любовь", 4800),
                new Product("Здоровье", 5100),
                new Product("Успех", 2100),
                new Product("Признание", 2400),
                new Product("Власть", 3500)
            };

            _db.AddRange(ProductsList);
            _db.SaveChanges();
        }
    }
}