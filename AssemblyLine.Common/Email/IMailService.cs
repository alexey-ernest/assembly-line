using System.Net.Mail;
using System.Threading.Tasks;

namespace AssemblyLine.Common.Email
{
    public interface IMailService
    {
        Task SendAsync(MailMessage message);
    }
}