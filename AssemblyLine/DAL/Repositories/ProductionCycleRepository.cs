using System.Data.Entity;
using System.Threading.Tasks;
using AssemblyLine.DAL.Entities;

namespace AssemblyLine.DAL.Repositories
{
    public class ProductionCycleRepository : IProductionCycleRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductionCycleRepository(ApplicationDbContext db)
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

        public Task<ProductionCycle> GetAsync()
        {
            return _db.ProductionCycles.FirstOrDefaultAsync();
        }
    }
}