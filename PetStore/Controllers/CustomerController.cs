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
    public class CustomerController : Controller
    {
        private readonly ICustomer _Customer;
        private readonly IBasket _Basket;
        public CustomerController(ICustomer _ICustomer,IBasket _IBasket)
        {
            _Customer = _ICustomer;
            _Basket = _IBasket;
        }
        public IActionResult Index()
        {
            return View(_Customer.GetCustomers);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Customer model)
        {
            if (ModelState.IsValid)
            {
                _Customer.Add(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public IActionResult Delete(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            else
            {
                return View(_Customer.GetCustomer(Id));
            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult ConfirmDelete(int? Id)
        {
            _Customer.Remove(Id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Details(int? Id) {
            return View(_Customer.GetCustomer(Id));
        }

        //action methods related to front end 
        public IActionResult CustomerLogin()
        {
            HttpContext.Session.Clear(); //clears session if any
            return View();
        }
        public IActionResult InitiateLogin(string custId)
        {
            bool success = int.TryParse(custId, out int custIdNum);

            Customer customerModel = _Customer.GetCustomer(custIdNum);
                if (customerModel==null)
                {
                //cannot find a valid customer
                ViewBag.ErrorMessage = "Please try again";
                    return View("CustomerLogin");
                }
                else
                {
                    //basket creation
                    Basket newBasket = new Basket() { Quantity = 0, OrderPlaced = OrderPlaced.No, SubTotal=0, Total=0,DateCreated=DateTime.Now,CustomerId= custIdNum };
                    int currentBasketId = _Basket.Add(newBasket);
                    newBasket.BasketId = currentBasketId;
                    //customer session creation
                
                    try
                    {
                        HttpContext.Session.SetString("activeCustomer", JsonConvert.SerializeObject(customerModel));
                        HttpContext.Session.SetString("activeBasket", JsonConvert.SerializeObject(newBasket));
                    }
                    catch (JsonSerializationException e)
                    {
                       //handle exception here
                    }
                //to avoid referencing loop
                //https://stackoverflow.com/questions/23453977/what-is-the-difference-between-preservereferenceshandling-and-referenceloophandl/23461179
                string activeUserData = JsonConvert.SerializeObject(customerModel, new JsonSerializerSettings(){
                        PreserveReferencesHandling=PreserveReferencesHandling.Objects,
                        Formatting=Formatting.Indented
                    });
                    string activeBasketData = JsonConvert.SerializeObject(newBasket, new JsonSerializerSettings()
                    {
                        PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                        Formatting = Formatting.Indented
                    });


                    return RedirectToAction("IndexCustomer", "Product");
                }
            
        }


    }
}