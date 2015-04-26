using System;
using System.Linq;
using System.Threading.Tasks;
using AssemblyLine.Common.Exceptions;
using AssemblyLine.DAL.Entities;

namespace AssemblyLine.DAL.Repositories
{
    /// <summary>
    /// Todo: implement
    /// </summary>
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

        public Task<Line> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Line> AddAsync(Line entity)
        {
            throw new NotImplementedException();
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

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}