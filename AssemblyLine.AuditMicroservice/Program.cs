using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AssemblyLine.Common.Audit;
using AssemblyLine.Common.Configuration;
using AssemblyLine.Common.Constants;
using AssemblyLine.Common.Initializers;
using Microsoft.ServiceBus.Messaging;

namespace AssemblyLine.AuditMicroservice
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
            string connectionString = configurationProvider.Get(SettingNames.ServiceBusConnectionString);

            // Create topic and subscription
            var initializer = new ServiceBusAuditInitializer(connectionString);
            initializer.Initialize();

            return SubscriptionClient.CreateFromConnectionString(connectionString, ServiceBusTopics.Audit,
                ServiceBusSubscriptions.AllMessages);
        }

        private static async Task ProcessMessageAsync(BrokeredMessage message)
        {
            AuditModel model;
            try
            {
                model = message.GetBody<AuditModel>();
            }
            catch (Exception e)
            {
                // ignoring invalid format messages
                Trace.TraceError("Invalid audit model for message {0}: {1}.", message.MessageId, e);
                message.Complete();
                return;
            }

            try
            {
                // Auditing
                var auditService = new TraceAuditService();
                await auditService.RecordAsync(model);

                // Print information
                Console.WriteLine();
                Console.WriteLine("MessageId: {0}", message.MessageId);
                Console.WriteLine("Date & time: {0}", model.DateTime);
                Console.WriteLine("User: {0}", model.User);
                Console.WriteLine("Object: {0}", model.ObjectType);
                Console.WriteLine("Object Id: {0}", model.ObjectId);

                // Remove message from subscription
                message.Complete();
            }
            catch (Exception e)
            {
                // Indicates a problem, unlock message in subscription
                Trace.TraceError("Could not process audit information: {0}", e);
                message.Abandon();
            }
        }
    }
}