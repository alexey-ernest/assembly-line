using System;
using System.Threading.Tasks;
using AssemblyLine.DAL.Repositories;

namespace AssemblyLine.BLL
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public Task KickOffProject(int id)
        {
            throw new NotImplementedException();
        }
    }
}