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
            return View();
        }

       /* //Просмотр каталога
        public IActionResult Catalog()
        {
            TestData.CreateTestData(_db);
            IEnumerable<Product> items = _db.Products;
            ViewBag.Products = items;
            return View("Catalog");
        }

        //Добавление товара в корзину
        public IActionResult Put(int id)
        {
            var product = _db.Products.Where(x => x.Id == id).FirstOrDefault();
            if (product == null) return RedirectToAction("Index");

            Order order = _db.Orders.Where(x => x.OrderStatus == Order.Status.Bag)
                                    .Include(x => x.Items)
                                    .ThenInclude(x => x.Product).FirstOrDefault();
            //Корзина пуста
            if (order == null)
            {
                order = new Order();
                order.OrderStatus = Order.Status.Bag;
                order.Items.Add(new OrderItem(product, 1));
                _db.Orders.Add(order);
            }
            //Корзина не пуста
            else
            {
                //Если товар уже встречался в корзине, то увеличиваем количество на 1
                var item = order.Items.Where(x => x.Product.Id == product.Id).FirstOrDefault();
                if (item == null) order.Items.Add(new OrderItem(product, 1));
                else item.Count++;
            }
            _db.SaveChanges();

            ViewData["Message"] = $"Товар \"{product.Name}\" успешно добавлен в корзину";
            return Catalog();
        }

        //Просмотр корзины
        public IActionResult Bag()
        {
            //создаем новую корзину
            Order order = new Order();
            order.OrderStatus = Order.Status.Bag;
            _db.Orders.Add(order);
            _db.SaveChanges();

            var item = _db.Orders.Where(x => x.OrderStatus == Order.Status.Bag)
                                 .Include(x => x.Items)
                                 .ThenInclude(x => x.Product).FirstOrDefault();
            BagViewModel bagVM = new BagViewModel
            {
                Items = item.Items
                        .Select(x => new BagItemViewModel
                        {
                            Id = x.Id,
                            Name = x.Product.Name,
                            Count = x.Count,
                            Price = x.Product.Price

                        }).ToList()
            };
            ViewBag.Bag = bagVM;
            return View();
        }

        //Удаление позиции из корзины
        public IActionResult DeleteBagItem(int id)
        {
            OrderItem item = _db.OrderItems.Find(id);
            if (item != null)
            {
                _db.OrderItems.Remove(item);
                _db.SaveChanges();
            }
            return RedirectToAction("Bag");
        }

        //Оформление заказа
        [HttpGet]
        public IActionResult Checkout()
        {
            return View();
        }

        //Оформление заказа
        [HttpPost]
        public IActionResult Checkout(Person person)
        {
            Order item = _db.Orders.Where(x => x.OrderStatus == Order.Status.Bag)
                                 .Include(x => x.Items)
                                 .ThenInclude(x => x.Product).FirstOrDefault();
            _db.Persons.Add(person);
            _db.SaveChanges();

            item.Person = person;
            item.OrderStatus = Order.Status.Confirmed;

            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();

            //создаем новую корзину
            Order order = new Order();
            order.OrderStatus = Order.Status.Bag;
            _db.Orders.Add(order);
            _db.SaveChanges();

            return View("Index");
        }*/

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
