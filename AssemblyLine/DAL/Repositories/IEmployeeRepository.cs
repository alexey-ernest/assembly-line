using System.Linq;
using System.Threading.Tasks;
using AssemblyLine.DAL.Entities;

namespace AssemblyLine.DAL.Repositories
{
    public interface IEmployeeRepository : IRepository
    {
        IQueryable<Employee> AsQueryable();

        Task<Employee> GetAsync(int id);

        Task<Employee> AddAsync(Employee entity);

        Task<Employee> EditAsync(Employee entity);

        Task DeleteAsync(int id);
    }
}