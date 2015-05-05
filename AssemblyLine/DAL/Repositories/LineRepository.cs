using System.Linq;
using System.Threading.Tasks;
using AssemblyLine.Common.Exceptions;
using AssemblyLine.DAL.Entities;

namespace AssemblyLine.DAL.Repositories
{
    public class LineRepository : ILineRepository
    {
        private readonly ApplicationDbContext _db;

        public LineRepository(ApplicationDbContext db)
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

        public IQueryable<Line> AsQueryable()
        {
            return _db.Lines;
        }

        public async Task<Line> GetAsync(int id)
        {
            return await _db.Lines.FindAsync(id);
        }

        public async Task<Line> AddAsync(Line entity)
        {
            entity = _db.Lines.Add(entity);
            await SaveChangesAsync();

            return entity;
        }

        public async Task<Line> EditAsync(Line entity)
        {
            var original = await _db.Lines.FindAsync(entity.Id);
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
            var entity = await _db.Lines.FindAsync(id);
            if (entity == null)
            {
                throw new NotFoundException(string.Format("Could not found object with id {0}", id));
            }

            _db.Lines.Remove(entity);
            await SaveChangesAsync();
        }
    }
}