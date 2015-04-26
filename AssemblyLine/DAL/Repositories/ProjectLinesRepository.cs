using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AssemblyLine.Common.Exceptions;
using AssemblyLine.DAL.Entities;

namespace AssemblyLine.DAL.Repositories
{
    public class ProjectLinesRepository : IProjectLinesRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IAssemblyLineTeamRepository _teamRepository;

        public ProjectLinesRepository(ApplicationDbContext db, IAssemblyLineTeamRepository temRepository)
        {
            _db = db;
            _teamRepository = temRepository;
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }

        public IQueryable<ProjectAssemblyLine> AsQueryable()
        {
            return _db.ProjectLines;
        }

        public Task<List<ProjectAssemblyLine>> GetByProjectAsync(int projectId)
        {
            return _db.ProjectLines.Where(l => l.Project.Id == projectId).ToListAsync();
        }

        public Task<ProjectAssemblyLine> GetAsync(int id)
        {
            return
                _db.ProjectLines.Include(l => l.Line)
                    .Include(l => l.Project)
                    .Include(l => l.ProductionTeam)
                    .Include(l => l.ProcurementTeam)
                    .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<ProjectAssemblyLine> EditAsync(ProjectAssemblyLine entity)
        {
            var original = await _db.ProjectLines.FindAsync(entity.Id);
            if (original == null)
            {
                throw new NotFoundException(string.Format("Could not found object with id {0}", entity.Id));
            }

            _db.Entry(original).CurrentValues.SetValues(entity);

            // updating navigation properties
            if (entity.ProductionTeam != null)
            {
                if (entity.ProductionTeam.Id > 0)
                {
                    entity.ProductionTeam = await _teamRepository.EditAsync(entity.ProductionTeam);
                }
                else
                {
                    original.ProductionTeam = await _teamRepository.AddAsync(entity.ProductionTeam);
                }
            }
            else
            {
                original.ProductionTeam = null;
            }

            if (entity.ProcurementTeam != null)
            {
                if (entity.ProcurementTeam.Id > 0)
                {
                    entity.ProcurementTeam = await _teamRepository.EditAsync(entity.ProcurementTeam);
                }
                else
                {
                    original.ProcurementTeam = await _teamRepository.AddAsync(entity.ProcurementTeam);
                }
            }
            else
            {
                original.ProcurementTeam = null;
            }

            await SaveChangesAsync();

            return original;
        }
    }
}