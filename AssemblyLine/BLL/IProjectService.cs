using System.Threading.Tasks;
using AssemblyLine.DAL.Entities;

namespace AssemblyLine.BLL
{
    public interface IProjectService
    {
        Task<Project> KickOffProject(int id);
    }
}