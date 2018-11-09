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
    //Класс для оформления заказа
    [Route("order")]
    public class OrderController : Controller
    {
        public ShopContext _db;

        public OrderController(ShopContext db)
        {
            _db = db;
        }

        [HttpGet]
        [Route("input")]
        public IActionResult InputDataPerson()
        {
            return View("DataPerson");
        }

        //Оформление заказа
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

            Order order = new Order();
            _db.Orders.Add(order);
            _db.SaveChanges();

            return View("Success");
        }

    }
}
