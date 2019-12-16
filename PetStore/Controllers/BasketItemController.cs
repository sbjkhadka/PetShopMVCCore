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
       
        //Basket pullBasket;
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
        
        public IActionResult AddToCart(int? pid,int? quantity,decimal? price) {
            if (quantity == null)
            {
                quantity = 1;
            }

            //basket item creation
            BasketItem bi = new BasketItem();
            bi.ItemQuantity = quantity;
            //bi.ItemQuantity = quantity==null?1:quantity;
            bi.ProductId = pid;
            bi.BasketId = getBasketIdFromSession(); 
            _BasketItem.Add(bi);
            //basket creation
            Basket pullBasket = new Basket() { Quantity = quantity,Total=price* quantity };
            //Basket pullBasket = new Basket() { Quantity = quantity == null ? 1 : quantity, Total = price * quantity == null ? 1 : quantity };
            _Basket.Update(bi.BasketId, pullBasket);
            //return RedirectToAction("Index");
            //return RedirectToAction("IndexCustomer", "Product");
            return RedirectToAction("getCustomerBasket", "Basket");
        }

        private int getBasketIdFromSession()
        {
            var currentBasket = JsonConvert.DeserializeObject<Basket>(HttpContext.Session.GetString("activeBasket"));
            return Convert.ToInt32(currentBasket.BasketId);
        }

        

    }
}