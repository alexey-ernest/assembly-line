using System;
using System.Threading.Tasks;

namespace AssemblyLine.DAL.Repositories
{
    public interface IRepository : IDisposable
    {
        Task SaveChangesAsync();
    }
}