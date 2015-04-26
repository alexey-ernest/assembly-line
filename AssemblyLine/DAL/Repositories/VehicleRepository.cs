using System;
using System.Linq;
using System.Threading.Tasks;
using AssemblyLine.DAL.Entities;

namespace AssemblyLine.DAL.Repositories
{
    /// <summary>
    /// Todo: implement
    /// </summary>
    public class VehicleRepository : IVehicleRepository
    {
        private readonly ApplicationDbContext _db;

        public VehicleRepository(ApplicationDbContext db)
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

        public IQueryable<Vehicle> AsQueryable()
        {
            return _db.Vehicles;
        }

        public Task<Vehicle> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Vehicle> AddAsync(Vehicle entity)
        {
            throw new NotImplementedException();
        }

        public Task<Vehicle> EditAsync(Vehicle entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}