using FitProDB.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitProDB.Repositories
{
    public class CustomerRepository
    {
        private IronBeastContext _context;

        public CustomerRepository(IronBeastContext context)
        {
            _context = context;
        }

        public async Task<Customer>GetCustomer(long id)
        {
            return await _context.Customers.FirstOrDefaultAsync(c => c.Id.Equals(id));
        }
        public async Task<Customer>AddCustomer(Customer customer)
        {
            var exists = await _context.Customers.AnyAsync(c => c.Name.Equals(customer.Name));
            if(exists)
            {
                return null;

            }

            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
            return new();
        }

        public async Task<Customer>Update(Customer customer)
        {
            var result = await _context.Customers.FirstOrDefaultAsync(c =>c .Id.Equals(customer.Id));
            if(result == null)
            {
                return null;
            }
            result.Name = customer.Name;
            result.LastName = customer.LastName;
            result.PhoneNumber = customer.PhoneNumber;
            result.Address = customer.Address;
            result.Email = customer.Email;

            _context.Customers.Update(result);
            await _context.SaveChangesAsync();
            return result;
        }

        public async Task<Customer>DeleteCustomer(int id)
        {
            var result = await _context.Customers.FirstOrDefaultAsync(c =>c .Id.Equals(id));
            if(result == null)
            {
                return null;
            }
            _context.Customers.Remove(result);
            await _context.SaveChangesAsync();
            return result;
        }
    }
}
