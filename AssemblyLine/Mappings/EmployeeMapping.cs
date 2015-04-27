using AssemblyLine.DAL.Entities;
using AssemblyLine.Models;

namespace AssemblyLine.Mappings
{
    public class EmployeeMapping : IMapping
    {
        public void CreateMap()
        {
            AutoMapper.Mapper.CreateMap<Employee, EmployeeListModel>()
                ;
        }
    }
}