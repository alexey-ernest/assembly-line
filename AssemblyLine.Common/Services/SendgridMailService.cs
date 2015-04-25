using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using SendGrid;

namespace AssemblyLine.Common.Services
{
    public class SendgridMailService : IMailService
    {
        private readonly string _password;
        private readonly string _username;

        public SendgridMailService(string username, string password)
        {
            _username = username;
            _password = password;
        }

        public async Task SendAsync(MailMessage message)
        {
            // Create the email object first, then add the properties.
            var sendGridMessage = new SendGridMessage {From = message.From};

            // Add multiple addresses to the To field.
            var recipients = message.To.Select(t => t.ToString());
            sendGridMessage.AddTo(recipients);

            sendGridMessage.Subject = message.Subject;

            //Add the HTML and Text bodies
            sendGridMessage.Html = message.IsBodyHtml ? message.Body : null;
            sendGridMessage.Text = message.IsBodyHtml ? null : message.Body;

            // Create network credentials to access your SendGrid account.
            var credentials = new NetworkCredential(_username, _password);

            // Create an Web transport for sending email.
            var transportWeb = new Web(credentials);

            // Send the email asynchronously
            await transportWeb.DeliverAsync(sendGridMessage);
        }
    }
}