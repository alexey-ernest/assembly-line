using System.Threading.Tasks;
using AssemblyLine.DAL.Entities;

namespace AssemblyLine.DAL.Repositories
{
    public interface IAssemblyLineTeamRepository : IRepository
    {
        Task<AssemblyLineTeam> EditAsync(AssemblyLineTeam entity);
    }
}