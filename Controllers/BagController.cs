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
    //Класс для работы с корзиной
    [Route("bag")]
    public class BagController : Controller
    {
        public ShopContext _db;

        public BagController(ShopContext db)
        {
            _db = db;
        }

        [HttpGet]
        [Route("")]
        //Просмотр корзины
        public IActionResult Index()
        {
            var bag = _db.Orders.Where(x => x.OrderStatus == Order.Status.Bag)
                                .Include(x => x.Items)
                                .ThenInclude(x => x.Product).FirstOrDefault();

            if (bag == null)
            {
                bag = new Order();
                _db.Orders.Add(bag);
                _db.SaveChanges();
            }

            BagViewModel bagVM = new BagViewModel
            {
                Items = bag.Items
                           .Select(x => new BagItemViewModel
                            {
                                Id = x.Id,
                                Name = x.Product.Name,
                                Count = x.Count,
                                Price = x.Product.Price

                            }).ToList()
            };
            ViewBag.Bag = bagVM;
            return View("Bag");
        }

        [HttpGet]
        [Route("delete/{id:int}")]
        //Удаление позиции из корзины
        public IActionResult Delete(int id)
        {
            OrderItem item = _db.OrderItems.Find(id);
            if (item != null)
            {
                _db.OrderItems.Remove(item);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
