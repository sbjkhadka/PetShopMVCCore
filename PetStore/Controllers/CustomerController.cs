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
        public CustomerController(ICustomer _ICustomer)
        {
            _Customer = _ICustomer;
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
            return View();
        }
        public IActionResult InitiateLogin(string custId)
        {
            bool success = int.TryParse(custId, out int custIdNum);

            Customer customerModel = _Customer.GetCustomer(custIdNum);
                if (customerModel==null)
                {
                    //cannot find a valid customer
                    return View("CustomerLogin");
                }
                else
                {
                    HttpContext.Session.SetString("activeCustomer", JsonConvert.SerializeObject(customerModel));
                    return RedirectToAction("IndexCustomer", "Product");
                }
            
           
            //ViewBag.activeCustomer = custId;
            //TempData["activeCustomer"] = custId;

            //session
            //pull customer Ids from customer table, loop through and compare with custNo
            //if found then set session, else do something else
            /*
            int custIdNum;
            bool success = int.TryParse(custId, out custIdNum);
            if (success)
            {
                var Customer = new Customer() { CustomerId = custIdNum };

                HttpContext.Session.SetString("activeCustomer", JsonConvert.SerializeObject(Customer));  //demo
            }
            else
            {
                //handle input validation here
            }
            */
            // Session["activeCustomer"] = custId;
            /*
            if (custNo == HttpContext.Session.GetString("activeCustomer"))//demo
            {
                return RedirectToAction("Create");
            }

            //ViewBag.Name = HttpContext.Session.GetString("test");
            //session
            return RedirectToAction("Index");
            */
            //return RedirectToAction("IndexCustomer", "Product");

        }


    }
}