using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AssemblyLine.Common.Exceptions;
using AssemblyLine.DAL.Entities;

namespace AssemblyLine.DAL.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApplicationDbContext _db;

        public ProjectRepository(ApplicationDbContext db)
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

        public IQueryable<Project> AsQueryable()
        {
            return _db.Projects;
        }

        public async Task<Project> GetAsync(int id)
        {
            return await _db.Projects.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Project> AddAsync(Project entity)
        {
            // setting default properties
            entity.Created = DateTime.UtcNow;
            entity.Status = ProjectStatus.New;

            entity = _db.Projects.Add(entity);
            await SaveChangesAsync();

            return entity;
        }

        public async Task<Project> EditAsync(Project entity)
        {
            var original = await _db.Projects.FindAsync(entity.Id);
            if (original == null)
            {
                throw new NotFoundException(string.Format("Could not found object with id {0}", entity.Id));
            }

            // Preserving non-modified properties
            entity.Created = original.Created;

            _db.Entry(original).CurrentValues.SetValues(entity);
            await SaveChangesAsync();

            return original;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _db.Projects.FindAsync(id);
            if (entity == null)
            {
                throw new NotFoundException(string.Format("Could not found object with id {0}", id));
            }

            _db.Projects.Remove(entity);
            await SaveChangesAsync();
        }
    }
}