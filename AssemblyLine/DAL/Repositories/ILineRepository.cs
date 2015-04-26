using System.Linq;
using System.Threading.Tasks;
using AssemblyLine.DAL.Entities;

namespace AssemblyLine.DAL.Repositories
{
    public interface ILineRepository : IRepository
    {
        IQueryable<Line> AsQueryable();

        Task<Line> GetAsync(int id);

        Task<Line> AddAsync(Line entity);

        Task<Line> EditAsync(Line entity);

        Task DeleteAsync(int id);
    }
}