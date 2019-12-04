using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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


    }
}