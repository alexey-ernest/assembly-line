using AssemblyLine.Mappings;
using Microsoft.Practices.Unity;
using Mapper = AutoMapper.Mapper;

namespace AssemblyLine
{
    public class MappingConfig
    {
        public static void Register(IUnityContainer container)
        {
            // creating maps
            container.RegisterType<IMapping, AssemblyLineMapping>("AssemblyLineMapping", new ContainerControlledLifetimeManager());
            container.RegisterType<IMapping, AssemblyLineTeamMapping>("AssemblyLineTeamMapping", new ContainerControlledLifetimeManager());
            container.RegisterType<IMapping, EmployeeMapping>("EmployeeMappings", new ContainerControlledLifetimeManager());
            container.RegisterType<IMapping, ProjectLineMapping>("ProjectLineMapping", new ContainerControlledLifetimeManager());
            container.RegisterType<IMapping, ProjectMapping>("ProjectMapping", new ContainerControlledLifetimeManager());
            container.RegisterType<IMapping, ProjectMilestoneMapping>("ProjectMilestoneMapping", new ContainerControlledLifetimeManager());

            var mappings = container.ResolveAll<IMapping>();
            foreach (var mapping in mappings)
            {
                mapping.CreateMap();
            }

            Mapper.AssertConfigurationIsValid();
        }
    }
}