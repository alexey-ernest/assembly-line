using System.Net.Mail;
using System.Threading.Tasks;

namespace AssemblyLine.Common.Services
{
    public interface IMailService
    {
        Task SendAsync(MailMessage message);
    }
}