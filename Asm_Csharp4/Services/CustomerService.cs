﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asm_Csharp4.Context;
using Asm_Csharp4.IServices;
using Asm_Csharp4.Models;
using Microsoft.AspNetCore.Mvc;

namespace Asm_Csharp4.Services
{
    public class CustomerService:ICustomerService
    {
        private readonly DatabaseContext _context;
        private List<Customers> lstCustomer;
        public CustomerService(DatabaseContext context)
        {
            _context = context;
            lstCustomer = new List<Customers>();
            GetListCustomers();
        }
        public List<Customers> GetListCustomers()
        {
            return  _context.Customers.ToList();
        }

        public Customers GetById(int? customerId)
        {
            return _context.Customers.SingleOrDefault(c => c.Id == customerId);
        }

        public Customers GetCustomerObj(int idCustomer)
        {
            return _context.Customers.Find(idCustomer);
        }

        public bool CheckIdCustomer(int idCustomer)
        {
            return _context.Customers.Any(c => c.Id == idCustomer);
        }

        public void Save(Customers customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
        }

        public void Update(Customers customer)
        {
            var cst = _context.Customers.Find(customer.Id);
            cst.FullName = customer.FullName;
            cst.Email = customer.Email;
            cst.Address = customer.Address;
            cst.SDT = customer.SDT;
            cst.CmndCccd = customer.CmndCccd;
            cst.Username = customer.Username;
            cst.Password = customer.Password;
            cst.Quyen = customer.Quyen;
            _context.Customers.Update(cst);
            _context.SaveChanges();
        }

        public int Delete(int customerId)
        {
            var cust = _context.Customers.Find(customerId);
            if (cust!=null)
            {
                _context.Customers.Remove(cust);
                _context.SaveChanges();
                return 0;
            }

            return -1;
        }
    }
}
