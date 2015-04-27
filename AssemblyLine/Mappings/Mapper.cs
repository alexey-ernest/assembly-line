using System.Linq;
using AutoMapper.QueryableExtensions;

namespace AssemblyLine.Mappings
{
    public class Mapper : IMapper
    {
        public TOutput Map<TInput, TOutput>(TInput source)
        {
            return AutoMapper.Mapper.Map<TInput, TOutput>(source);
        }

        public IQueryable<TOutput> Project<TInput, TOutput>(IQueryable<TInput> source)
        {
            return source.Project().To<TOutput>();
        }
    }
}