using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssemblyLine.Common.Exceptions;
using AssemblyLine.DAL.Entities;
using AssemblyLine.DAL.Repositories;
using TaskStatus = AssemblyLine.DAL.Entities.TaskStatus;

namespace AssemblyLine.BLL
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProductionCycleRepository _productionCycleRepository;

        public ProjectService(IProjectRepository projectRepository, IProductionCycleRepository productionCycleRepository)
        {
            _projectRepository = projectRepository;
            _productionCycleRepository = productionCycleRepository;
        }

        public async Task<Project> KickOffProject(int id)
        {
            var project = await _projectRepository.GetAsync(id);
            if (project == null)
            {
                throw new NotFoundException(string.Format("Could not find project with id: {0}.", id));
            }

            var cycle = await _productionCycleRepository.GetAsync();
            if (cycle == null)
            {
                throw new NotFoundException("Could not find production cycle template.");
            }

            if (!cycle.Milestones.Any())
            {
                throw new NotFoundException("Could not find any milestones in production cycle.");
            }

            // Copying cycle into the project
            var projectCycle = new ProjectProductionCycle
            {
                Milestones = new List<ProjectCycleMilestone>()
            };

            project.Cycle = projectCycle;
            project = await _projectRepository.EditAsync(project);

            foreach (var milestone in cycle.Milestones)
            {
                var projectMilestone = new ProjectCycleMilestone
                {
                    Status = MilestoneStatus.NotStarted,
                    Name = milestone.Name,
                    Position = milestone.Position,
                    Tasks = new List<ProjectMilestoneTask>()
                };

                foreach (var task in milestone.Tasks)
                {
                    var projectMilestoneTask = new ProjectMilestoneTask
                    {
                        Status = TaskStatus.NotStarted,
                        Name = task.Name
                    };
                    projectMilestone.Tasks.Add(projectMilestoneTask);
                }

                projectCycle.Milestones.Add(projectMilestone);
            }

            project = await _projectRepository.EditAsync(project);

            // Start cycle and first milestone
            var firstMilestone = projectCycle.Milestones.OrderBy(m => m.Position).First();
            firstMilestone.Started = DateTime.UtcNow;
            firstMilestone.Status = MilestoneStatus.InProgress;
            
            projectCycle.Milestone = firstMilestone;
            projectCycle.Started = DateTime.UtcNow;
            projectCycle.Status = CycleStatus.InProgress;

            project.Cycle = projectCycle;
            project.Started = DateTime.UtcNow;
            project.Status = ProjectStatus.InProgress;

            project = await _projectRepository.EditAsync(project);

            return project;
        }
    }
}