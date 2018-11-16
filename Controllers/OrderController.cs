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
    [Route("order")]
    public class OrderController : Controller
    {
        public ShopContext _db;

        public OrderController(ShopContext db)
        {
            _db = db;
        }

        #region Оформление заказа
        [HttpGet]
        [Route("input")]
        public IActionResult InputDataPerson()
        {
            return View("DataPerson");
        }

        [HttpPost]
        [Route("confirm")]
        public IActionResult Confirm(Person person)
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

            return View("Success");
        }
        #endregion

        #region История заказов
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            IEnumerable<Order> items = _db.Orders.Include(x => x.Items)
                                                 .ThenInclude(x => x.Product)
                                                 .Where(x => x.OrderStatus != Order.Status.Bag)
                                                 .OrderByDescending(x=>x.Number).ToList();

            var ordersViewModel = items
                .Select(order => new BagViewModel
                {
                    Number = order.Number,
                    OrderStatus = order.OrderStatus,
                    Items = order.Items
                        .Select(item => new BagItemViewModel
                        {
                            Name = item.Product.Name,
                            Price = item.Product.Price,
                            Count = item.Count
                        }).ToList()
                });

            return View(ordersViewModel);
        }
        #endregion

    }
}
