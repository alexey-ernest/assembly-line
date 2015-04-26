using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AssemblyLine.Common.Exceptions;
using AssemblyLine.DAL.Entities;

namespace AssemblyLine.DAL.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _db;

        public EmployeeRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }

        public IQueryable<Employee> AsQueryable()
        {
            return _db.Employees;
        }

        public async Task<Employee> GetAsync(int id)
        {
            return await _db.Employees.FirstOrDefaultAsync(a => a.Id == id);
        }

        public Task<Employee> AddAsync(Employee entity)
        {
            // setting default properties
            entity.Created = DateTime.UtcNow;

            _db.Employees.Add(entity);

            return Task.FromResult(entity);
        }

        public async Task<Employee> EditAsync(Employee entity)
        {
            var original = await _db.Employees.FindAsync(entity.Id);
            if (original == null)
            {
                throw new NotFoundException(string.Format("Could not found object with id {0}", entity.Id));
            }

            // Preserving non-modified properties
            entity.Created = original.Created;

            _db.Entry(original).CurrentValues.SetValues(entity);

            return original;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _db.Employees.FindAsync(id);
            if (entity == null)
            {
                throw new NotFoundException(string.Format("Could not found object with id {0}", id));
            }

            _db.Employees.Remove(entity);
        }
    }
}