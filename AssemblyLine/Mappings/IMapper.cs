using System.Linq;

namespace AssemblyLine.Mappings
{
    public interface IMapper
    {
        TOutput Map<TInput, TOutput>(TInput source);

        IQueryable<TOutput> Project<TInput, TOutput>(IQueryable<TInput> source);
    }
}