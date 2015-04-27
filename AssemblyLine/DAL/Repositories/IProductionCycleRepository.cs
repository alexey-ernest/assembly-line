using System.Threading.Tasks;
using AssemblyLine.DAL.Entities;

namespace AssemblyLine.DAL.Repositories
{
    public interface IProductionCycleRepository : IRepository
    {
        Task<ProductionCycle> GetAsync();
    }
}