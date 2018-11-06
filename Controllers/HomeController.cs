using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;
using Shop.ViewModels;

namespace Shop.Controllers
{
    public class HomeController : Controller
    {
        public ShopContext _db;

        public HomeController(ShopContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            TestData.CreateTestData(_db);
            return View();
        }

        public IActionResult Catalog()
        {
            IEnumerable<Product> items = _db.Products;
            ViewBag.Products = items;
            return View("Catalog");
        }

        public IActionResult Put(int id)
        {
            var item = _db.Products.Where(x => x.Id == id).FirstOrDefault();
            if (item == null) return Redirect("Index");

            OrderItem orderItem = new OrderItem(item, 1);
            Order order = new Order();
            order.Items.Add(orderItem);

            _db.Orders.Add(order);
            _db.SaveChanges();

            ViewData["Message"] = $"Товар \"{item.Name}\" успешно добавлен в корзину";

            return Catalog();
        }

        public IActionResult Basket()
        {
            var item = _db.Orders.Include(x => x.Items)
                                 .ThenInclude(x => x.Product).FirstOrDefault();

            BasketViewModel basketVM = new BasketViewModel
            {
                Items = item.Items
                        .Select(x => new BasketItemViewModel
                        {
                            Name = x.Product.Name,
                            Count = x.Count,
                            Price = x.Product.Price,

                        }).ToList()
            };
            ViewBag.Basket = basketVM;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
