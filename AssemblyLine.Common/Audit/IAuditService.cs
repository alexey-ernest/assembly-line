using System.Threading.Tasks;

namespace AssemblyLine.Common.Audit
{
    public interface IAuditService
    {
        Task RecordAsync(AuditModel data);
    }
}