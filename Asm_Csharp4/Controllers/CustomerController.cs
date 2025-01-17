﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asm_Csharp4.Context;
using Asm_Csharp4.IServices;
using Asm_Csharp4.Models;
using Asm_Csharp4.Services;
using Microsoft.AspNetCore.Http;

namespace Asm_Csharp4.Controllers
{
    public class CustomerController : Controller
    {
        private readonly DatabaseContext _context;
        private ICustomerService _iCustomerService;

        public CustomerController(DatabaseContext context)
        {
            _context = context;
            _iCustomerService = new CustomerService(_context);
        }
        public IActionResult Index()
        {

            try
            {
                var lstCustomers = _iCustomerService.GetListCustomers();
                return View(lstCustomers);
            }
            catch (Exception e)
            {
                return View();
            }
        }
    
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("FullName", "Email", "SDT", "CmndCCCD", "Address", "Username", "Password", "Quyen")] Customers customer)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(customer);
                }
                if (customer.Id == 0)
                {
                    _iCustomerService.Save(customer);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return View(customer);
            }
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {

            var customer = _iCustomerService.GetCustomerObj(id);
            return View(customer);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult EditCustomer(Customers customer)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(customer);
                }
                _iCustomerService.Update(customer);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return View(customer);
            }
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {

            var customer = _iCustomerService.GetCustomerObj(id);
            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCustomer(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (_iCustomerService.CheckIdCustomer(id))
                    {
                        _iCustomerService.Delete(id);
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return RedirectToAction("Index");
            }
        }
    }
}
