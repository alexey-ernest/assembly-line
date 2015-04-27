using System.Threading.Tasks;

namespace AssemblyLine.BLL
{
    public interface IProjectService
    {
        Task KickOffProject(int id);
    }
}