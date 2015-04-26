using System;
using System.Threading.Tasks;

namespace AssemblyLine.Common.Logging
{
    public interface ILogService
    {
        Task WriteAsync(string message);

        Task WriteAsync(Exception exception);

        Task WriteAsync(Exception exception, Guid correlationToken);
    }
}