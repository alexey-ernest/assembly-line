using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssemblyLine.DAL.Entities;

namespace AssemblyLine.DAL.Repositories
{
    public interface IProjectLinesRepository : IRepository
    {
        IQueryable<ProjectLine> AsQueryable();

        Task<List<ProjectLine>> GetByProjectAsync(int projectId);

        Task<ProjectLine> GetAsync(int id);

        Task<ProjectLine> GetWithMilestoneTasksAsync(int id);

        Task<ProjectLine> EditAsync(ProjectLine entity);
    }
}