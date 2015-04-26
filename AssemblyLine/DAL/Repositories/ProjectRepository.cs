using System;
using System.Collections.Generic;
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

            // allocating assembly lines
            if (entity.AssemblyLinesNumber.HasValue)
            {
                var lines = await AllocateAssemblyLinesAsync(entity.AssemblyLinesNumber.Value);
                entity.AssemblyLines = lines;
            }

            entity = _db.Projects.Add(entity);
            await SaveChangesAsync();

            return entity;
        }

        private async Task<List<ProjectAssemblyLine>> AllocateAssemblyLinesAsync(int number)
        {
            // trying allocate free lines
            var lines = await _db.Lines.OrderBy(l => l.Status).ToListAsync();
            if (lines.Count < number)
            {
                throw new BadRequestException("Not enough assembly lines");
            }

            var projectLines = new List<ProjectAssemblyLine>();
            for (var i = 0; i < number; i++)
            {
                var line = new ProjectAssemblyLine
                {
                    Line = lines[i]
                };
                projectLines.Add(line);
            }

            return projectLines;
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

            // updating navigation properties
            original.Vehicle = entity.Vehicle != null ? await _db.Vehicles.FindAsync(entity.Vehicle.Id) : null;

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