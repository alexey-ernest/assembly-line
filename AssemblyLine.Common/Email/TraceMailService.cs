using System.Diagnostics;
using System.Net.Mail;
using System.Threading.Tasks;

namespace AssemblyLine.Common.Email
{
    public class TraceMailService : IMailService
    {
        public Task SendAsync(MailMessage message)
        {
            Trace.TraceInformation("Outgoing mail message: \nSubject - {0}, \nTo - {1}, \nFrom - {2}, \nBody - {3}",
                message.Subject, string.Join(", ", message.To), message.From, message.Body);
            return Task.FromResult(0);
        }
    }
}