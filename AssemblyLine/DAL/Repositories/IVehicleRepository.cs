using System.Linq;
using System.Threading.Tasks;
using AssemblyLine.DAL.Entities;

namespace AssemblyLine.DAL.Repositories
{
    public interface IVehicleRepository : IRepository
    {
        IQueryable<Vehicle> AsQueryable();

        Task<Vehicle> GetAsync(int id);

        Task<Vehicle> AddAsync(Vehicle entity);

        Task<Vehicle> EditAsync(Vehicle entity);

        Task DeleteAsync(int id);
    }
}