using Accounting.DataLayer;
using Accounting.DataLayer.Context;
using Accounting.DataLayer.Repositories;
using Accounting.DataLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {




           // Accounting_DBEntities dbEntities = new Accounting_DBEntities();
           // ICustomerRepository customer = new CustomerRepository(dbEntities);
            
           //Customers AddCustoms = new Customers() {FullName="arman" , CustomerImage="nophoto", Mobile = "09121212121"};
           //customer.InsertCustomer(AddCustoms);
           //// customer.Save();
            
           // var list = customer.GetAllCustomers();





            UnitOFWork db = new UnitOFWork();
            var list = db.CustomerRepository.GetAllCustomers();
            db.Dispose();


        }
    }
}
