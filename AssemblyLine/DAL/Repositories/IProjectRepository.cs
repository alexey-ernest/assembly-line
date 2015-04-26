using System.Linq;
using System.Threading.Tasks;
using AssemblyLine.DAL.Entities;

namespace AssemblyLine.DAL.Repositories
{
    public interface IProjectRepository : IRepository
    {
        IQueryable<Project> AsQueryable();

        Task<Project> GetAsync(int id);

        Task<Project> AddAsync(Project entity);

        Task<Project> EditAsync(Project entity);

        Task DeleteAsync(int id);
    }
}