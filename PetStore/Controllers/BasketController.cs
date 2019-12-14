using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PetStore.Models;
using PetStore.Services;

namespace PetStore.Controllers
{
    public class BasketController : Controller
    {
        private readonly IBasket _Basket;
        private readonly ICustomer _Customer;
        public BasketController(IBasket _IBasket, ICustomer _ICustomer)
        {
            _Basket = _IBasket;
            _Customer = _ICustomer;
        }
        public IActionResult Index()
        {
            return View(_Basket.GetBaskets);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Customers = _Customer.GetCustomers;
            return View();
        }
        [HttpPost]
        public IActionResult Create(Basket model)
        {
            if (ModelState.IsValid)
            {
                _Basket.Add(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        //public int CreateBasket(Basket model) {
        //    if (ModelState.IsValid)
        //    {
        //       return  _Basket.Add(model);
        //        //return RedirectToAction("Index");
        //    }
        //    else {
        //        return 1;
        //    }
            
        //}
        [HttpGet]
        public IActionResult Delete(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            else
            {
                return View(_Basket.GetBasket(Id));
            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirm(int Id)
        {
            _Basket.Remove(Id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Details(int? Id)
        {
            return View(_Basket.GetBasket(Id));
        }
        
    }
}