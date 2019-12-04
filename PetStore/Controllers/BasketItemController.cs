using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PetStore.Models;
using PetStore.Services;

namespace PetStore.Controllers
{
    public class BasketItemController : Controller
    {
        private readonly IBasketItem _BasketItem;
        private readonly IProduct _Product;
        private readonly IBasket _Basket;
        public BasketItemController(IBasketItem _IBasketItem,IProduct _IProduct,IBasket _IBasket)
        {
            _BasketItem = _IBasketItem;
            _Product = _IProduct;
            _Basket = _IBasket;
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
    }
}