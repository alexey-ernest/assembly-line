using System.Linq;
using System.Threading.Tasks;
using AssemblyLine.Common.Exceptions;
using AssemblyLine.DAL.Entities;

namespace AssemblyLine.DAL.Repositories
{
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

        public async Task<Vehicle> GetAsync(int id)
        {
            return await _db.Vehicles.FindAsync(id);
        }

        public async Task<Vehicle> AddAsync(Vehicle entity)
        {
            entity = _db.Vehicles.Add(entity);
            await SaveChangesAsync();

            return entity;
        }

        public async Task<Vehicle> EditAsync(Vehicle entity)
        {
            var original = await _db.Vehicles.FindAsync(entity.Id);
            if (original == null)
            {
                throw new NotFoundException(string.Format("Could not found object with id {0}", entity.Id));
            }

            _db.Entry(original).CurrentValues.SetValues(entity);
            await SaveChangesAsync();

            return original;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _db.Vehicles.FindAsync(id);
            if (entity == null)
            {
                throw new NotFoundException(string.Format("Could not found object with id {0}", id));
            }

            _db.Vehicles.Remove(entity);
            await SaveChangesAsync();
        }
    }
}