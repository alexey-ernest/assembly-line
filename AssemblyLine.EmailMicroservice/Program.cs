using System;
using System.Diagnostics;
using System.Net.Mail;
using System.Threading.Tasks;
using AssemblyLine.Common.Configuration;
using AssemblyLine.Common.Constants;
using AssemblyLine.Common.Email;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;

namespace AssemblyLine.EmailMicroservice
{
    internal class Program
    {
        private static void Main()
        {
            // Get subscription client
            SubscriptionClient subscriptionClient;
            try
            {
                subscriptionClient = EsteblishConnection();
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not establish connection for service bus: {0}", e);
                return;
            }
            

            // Configure the callback options
            var options = new OnMessageOptions
            {
                AutoComplete = false
            };

            Console.WriteLine("Press any key to exit.\n\n");

            // Listening for messages
            subscriptionClient.OnMessageAsync(ProcessMessageAsync, options);

            Console.ReadKey();
        }

        private static SubscriptionClient EsteblishConnection()
        {
            var configurationProvider = new ConfigurationProvider();
            var connectionString = configurationProvider.Get(SettingNames.ServiceBusConnectionString);

            // Create topic and subscription
            var namespaceManager = NamespaceManager.CreateFromConnectionString(connectionString);
            if (!namespaceManager.TopicExists(ServiceBusTopics.Email))
            {
                namespaceManager.CreateTopic(ServiceBusTopics.Email);
            }
            if (!namespaceManager.SubscriptionExists(ServiceBusTopics.Email, ServiceBusSubscriptions.AllMessages))
            {
                namespaceManager.CreateSubscription(ServiceBusTopics.Email, ServiceBusSubscriptions.AllMessages);
            }
            
            return SubscriptionClient.CreateFromConnectionString(connectionString, ServiceBusTopics.Email, ServiceBusSubscriptions.AllMessages);
        }

        private static async Task ProcessMessageAsync(BrokeredMessage message)
        {
            EmailModel model;
            try
            {
                model = message.GetBody<EmailModel>();
            }
            catch (Exception e)
            {
                // ignoring invalid format messages
                Trace.TraceError("Invalid email model for message {0}: {1}.", message.MessageId, e);
                message.Complete();
                return;
            }

            try
            {
                // Send email
                var emailMessage = new MailMessage
                {
                    From = new MailAddress(model.From),
                    Subject = model.Subject,
                    Body = model.Body,
                    IsBodyHtml = true
                };

                foreach (var emailTo in emailMessage.To)
                {
                    emailMessage.To.Add(emailTo);
                }

                var mailService = new TraceMailService();
                await mailService.SendAsync(emailMessage);

                // Print information
                Console.WriteLine();
                Console.WriteLine("MessageId: {0}", message.MessageId);
                Console.WriteLine("Subject: {0}", model.Subject);
                Console.WriteLine("To: {0}", string.Join(", ", model.To));

                // Remove message from subscription
                message.Complete();
            }
            catch (Exception e)
            {
                // Indicates a problem, unlock message in subscription
                Trace.TraceError("Could not process outgoing email message: {0}", e);
                message.Abandon();
            }
        }
    }
}