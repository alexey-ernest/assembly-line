using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AssemblyLine.Common.Logging
{
    public class TraceLogService : ILogService
    {
        public Task WriteAsync(string message)
        {
            Trace.TraceInformation(message);
            return Task.FromResult(0);
        }

        public Task WriteAsync(Exception exception)
        {
            Trace.TraceError(exception.Message);
            return Task.FromResult(0);
        }

        public Task WriteAsync(Exception exception, Guid correlationToken)
        {
            Trace.TraceError("Exception correlation token: {0}. Details: {1}", correlationToken, exception.Message);
            return Task.FromResult(0);
        }
    }
}