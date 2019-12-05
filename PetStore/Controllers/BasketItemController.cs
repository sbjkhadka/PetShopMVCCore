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
        
        public IActionResult AddToCart(int? pid,int? quantity,string custNo) {
            BasketItem bi = new BasketItem();
            bi.ItemQuantity = quantity;
            bi.ProductId = pid;
            bi.BasketId = 3;
            _BasketItem.Add(bi);


            //session
            //pull customer Ids from customer table, loop through and compare with custNo
            //if found then set session, else do something else
            HttpContext.Session.SetString("test", "8");  //demo
            
            if (custNo == HttpContext.Session.GetString("test"))//demo
            {
                return RedirectToAction("Create");
            }

            //ViewBag.Name = HttpContext.Session.GetString("test");
            //session
            return RedirectToAction("Index");
        }
    }
}