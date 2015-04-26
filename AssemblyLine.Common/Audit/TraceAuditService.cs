using System.Diagnostics;
using System.Threading.Tasks;

namespace AssemblyLine.Common.Audit
{
    // Todo: should be replaced with corporate audit service.
    public class TraceAuditService: IAuditService
    {
        public Task RecordAsync(AuditModel data)
        {
            Trace.TraceInformation("Audit information: \ndate - {0}, \nuser - {1}, \nobject - {2}, \nobject id - {3}, \noriginal value - {4}, \nupdated value - {5}");
            return Task.FromResult(0);
        }
    }
}