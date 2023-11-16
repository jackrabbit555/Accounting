using Accounting.DataLayer.Repositories;
using Accounting.DataLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.DataLayer.Context
{
    public class UnitOFWork : IDisposable
    {

        Accounting_DBEntities db = new Accounting_DBEntities();


        private ICustomerRepository _customerRepository;
        public ICustomerRepository CustomerRepository 

            

        { 
        get {
                if (_customerRepository == null)
                {
                    _customerRepository = new CustomerRepository(db);
                }
                return _customerRepository;
            }
        
        }
       


        private GenericRepository<Accounting>_accountingRepository;
        public GenericRepository<Accounting> AccountingRepository 
        { get 
            
            {
                if (_accountingRepository == null)
                {
                    _accountingRepository = new GenericRepository<Accounting>(db);
                }

                return _accountingRepository;
            } 
        
        
        
        }



        private GenericRepository<Login> _LoginRepository;
        public GenericRepository<Login> LoginRopository { get 
            {
                if (_LoginRepository == null) 
                {
                    _LoginRepository = new GenericRepository<Login>(db);
                }
                return (_LoginRepository);
            } 
        }




        public void Save()
        {
            db.Configuration.ValidateOnSaveEnabled = false;
            db.SaveChanges();
        }


        public void Dispose()
        {
            db.Dispose();
        }
    }
}
