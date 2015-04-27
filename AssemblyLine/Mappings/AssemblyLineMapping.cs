using AssemblyLine.DAL.Entities;
using AssemblyLine.Models;

namespace AssemblyLine.Mappings
{
    public class AssemblyLineMapping : IMapping
    {
        public void CreateMap()
        {
            AutoMapper.Mapper.CreateMap<Line, AssemblyLineModel>()
                ;
        }
    }
}