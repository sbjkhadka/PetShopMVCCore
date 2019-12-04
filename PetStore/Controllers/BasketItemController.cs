using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PetStore.Models;
using PetStore.Services;

namespace PetStore.Controllers
{
    public class BasketItemController : Controller
    {
        private readonly IBasketItem _BasketItem;
        private readonly IProduct _Product;
        private readonly IBasket _Basket;
        private readonly ICustomer _Customer;
        public BasketItemController(IBasketItem _IBasketItem,IProduct _IProduct,IBasket _IBasket, ICustomer _ICustomer)
        {
            _BasketItem = _IBasketItem;
            _Product = _IProduct;
            _Basket = _IBasket;
            _Customer = _ICustomer;
        }
        public IActionResult Index()
        {
            
            return View(_BasketItem.GetBasketItems);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Products = _Product.GetProducts;
            ViewBag.Baskets = _Basket.GetBaskets;
            return View();
        }
        [HttpPost]
        public IActionResult Create(BasketItem model)
        {
            if (ModelState.IsValid)
            {
                _BasketItem.Add(model);
                return RedirectToAction("Index");
            }
            return View(model);

        }
        [HttpGet]
        public IActionResult Delete(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            else
            {
                return View(_BasketItem.GetBasketItem(Id));
            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirm(int? Id)
        {
            _BasketItem.Remove(Id);
            return RedirectToAction("Index");
        }

        //front-end related
        
        public IActionResult AddToCart(int? pid,int? quantity, int custId) {
            BasketItem bi = new BasketItem();
            bi.ItemQuantity = quantity;
            bi.ProductId = pid;
            bi.BasketId = 3;
            _BasketItem.Add(bi);
            //ViewBag.CustomerList = _Customer.GetCustomers;

            //session

            //const string sessionKey = "CustID";
            //int id = custId;
            //var value = HttpContext.Session.GetString(sessionKey);
            //if (string.IsNullOrEmpty(value))
            //{
            //    //dateFirstSeen = DateTime.Now;
            //    var serialisedID = JsonConvert.SerializeObject(id);
            //    HttpContext.Session.SetString(sessionKey, serialisedID);
            //}
            //else
            //{
            //    id = 8;
            //}

            
            //if (_Session.TryGetValue("CustID", out int a))
            //{
            //    return RedirectToAction("Index");
            //}
            //else
            //{
            //    return RedirectToAction("Create");
            //}
            //return View(model);
            //session
            return RedirectToAction("Index");
        }
    }
}