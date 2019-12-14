﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetStore.Models;
using PetStore.Services;
using System.Web;
using Newtonsoft.Json;

namespace PetStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProduct _Product;
        public ProductController(IProduct _IProduct)
        {
            _Product = _IProduct;
        }
        public IActionResult Index()
        {
            return View(_Product.GetProducts);
        }
        
        [HttpGet]
        public IActionResult Create()
        {
            Product model = new Product();
            model.ProductId = 0;
            return View(model);
        }
        //[HttpPost]
        //public IActionResult Create(Product model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _Product.Add(model);
        //        return RedirectToAction("Index");
        //    }
        //    return View(model);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(int Id,[Bind("ProductId,ProductName,Price,Description")] Product product,IFormFile imageFile) {
            if (Id != product.ProductId)
            {
                return NotFound();
            }
            if (ModelState.IsValid) {
                try {
                    if (imageFile != null) {
                        using (var ms = new MemoryStream()) {
                            imageFile.CopyTo(ms);
                            product.ProductImage = ms.ToArray();
                        }
                         _Product.Add(product);
                    }
                } catch (Exception ex) {
                    throw;
                }
            }
            return View(product);
        }
        public IActionResult Delete(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            else
            {
                return View(_Product.GetProduct(Id));
            }
            
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult ConfirmDelete(int? Id) {
            _Product.Remove(Id);
            return RedirectToAction("Index");

        }
        public IActionResult Edit(int? Id)
        {
            var model = _Product.GetProduct(Id);
            return View("create", model);
        }

        //customer side 
        public IActionResult IndexCustomer() {
            if (HttpContext.Session.GetString("activeCustomer") != null && HttpContext.Session.GetString("activeBasket")!=null)
            {
                var loggedIn = JsonConvert.DeserializeObject<Customer>(HttpContext.Session.GetString("activeCustomer"));
                int customerID = Convert.ToInt32(loggedIn.CustomerId);
                string customerName = Convert.ToString(loggedIn.FirstName);
                ViewBag.cid = customerName;

                var currentBasket = JsonConvert.DeserializeObject<Basket>(HttpContext.Session.GetString("activeBasket"));
                int BasketId = Convert.ToInt32(currentBasket.BasketId);
                ViewBag.bid = BasketId;
                return View(_Product.GetProducts);
            }
            else
            {
                
                return RedirectToAction("CustomerLogin", "Customer");
                //mesagebox
            }
                
            
           
            

            
        }
        [HttpGet]
        public IActionResult DetailsCustomer(int? Id) {
            Product model = _Product.GetProduct(Id);
            ViewBag.selectedProduct = model;

            var loggedIn = JsonConvert.DeserializeObject<Customer>(HttpContext.Session.GetString("activeCustomer"));
            int customerID = Convert.ToInt32(loggedIn.CustomerId);
            string customerName = Convert.ToString(loggedIn.FirstName);
            ViewBag.cid = customerName;

            var currentBasket = JsonConvert.DeserializeObject<Basket>(HttpContext.Session.GetString("activeBasket"));
            int BasketId = Convert.ToInt32(currentBasket.BasketId);
            ViewBag.bid = BasketId;
            


            return View(model);
        }
    }
}