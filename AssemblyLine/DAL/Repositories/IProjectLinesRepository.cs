using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssemblyLine.DAL.Entities;

namespace AssemblyLine.DAL.Repositories
{
    public interface IProjectLinesRepository : IRepository
    {
        IQueryable<ProjectAssemblyLine> AsQueryable();

        Task<List<ProjectAssemblyLine>> GetByProjectAsync(int projectId);

        Task<ProjectAssemblyLine> GetAsync(int id);

        Task<ProjectAssemblyLine> EditAsync(ProjectAssemblyLine entity);
    }
}