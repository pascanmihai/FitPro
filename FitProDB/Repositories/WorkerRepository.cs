using FitProDB.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitProDB.Repositories
{
    public class WorkerRepository
    {
        private IronBeastContext _context;

        public WorkerRepository(IronBeastContext context)
        {
            _context = context;
        }

        public async Task<Worker>GetWorker(long id)
        {
            return await _context.Workers.FirstOrDefaultAsync(w => w.Id.Equals(id));
        }

        public async Task<Worker>AddWorker(Worker worker)
        {
            var exists = await _context.Workers.AnyAsync(w => w.Name.Equals(worker.Name));
            if(exists)
            {
                return null;
            }

            await _context.Workers.AddAsync(worker);    
            await _context.SaveChangesAsync();
            return new();
        }

        public async Task<Worker>UpdateWorker(Worker worker)
        {
            var result = await _context.Workers.FirstOrDefaultAsync(w => w.Id.Equals(worker.Id));
            if(result== null)
            {
                return null;
            }
            result.Name = worker.Name;
            result.LastName = worker.LastName;
            result .Email = worker.Email;
            result.Address = worker.Address;
            result.PhoneNumber = worker.PhoneNumber;

            _context.Workers.Update(result);
            await _context.SaveChangesAsync();
            return result;
        }

        public async Task<Worker>DeleteWorker(int id)
        {
            var result = await _context.Workers.FirstOrDefaultAsync(w => w.Id.Equals(id));
            if(result == null)
            {
                return null;
            }

            _context.Workers.Remove(result);
            await _context.SaveChangesAsync();
            return result;
        }
    }
}
