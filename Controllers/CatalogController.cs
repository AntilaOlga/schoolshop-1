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
    //Класс для работы с каталогом товаров
    [Route("catalog")]
    public class CatalogController : Controller
    {
        public ShopContext _db;

        public CatalogController(ShopContext db)
        {
            _db = db;
        }

        [HttpGet]
        [Route("")]
        //Просмотр каталога
        public IActionResult Index()
        {
            TestData.CreateTestData(_db);

            IEnumerable<Product> items = _db.Products;
            //ViewBag.Products = items;
            return View("Catalog", items);
        }

        [HttpGet]
        [Route("add/{id:int}")]
        //Добавление товара в корзину
        public IActionResult Add(int id)
        {
            var product = _db.Products.Where(x => x.Id == id).FirstOrDefault();
            if (product == null) return RedirectToAction("Index");

            Order bag = _db.Orders.Where(x => x.OrderStatus == Order.Status.Bag)
                                    .Include(x => x.Items)
                                    .ThenInclude(x => x.Product).FirstOrDefault();
            if (bag == null)
            {
                bag = new Order();
                bag.Items.Add(new OrderItem(product, 1));
                _db.Orders.Add(bag);
            }
            else
            {
                //Если товар уже встречался в корзине, то увеличиваем количество на 1
                var item = bag.Items.Where(x => x.Product.Id == product.Id).FirstOrDefault();
                if (item == null) bag.Items.Add(new OrderItem(product, 1));
                else item.Count++;
            }
            _db.SaveChanges();

            //ViewData["Message"] = $"Товар \"{product.Name}\" успешно добавлен в корзину";
            return Index();
        }
    }
}
