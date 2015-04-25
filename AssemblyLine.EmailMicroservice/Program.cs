using System;
using AssemblyLine.Common.Configuration;
using AssemblyLine.Common.Constants;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;

namespace AssemblyLine.EmailMicroservice
{
    internal class Program
    {
        private static void Main()
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

            // Get subscription client
            var subscriptionClient = SubscriptionClient.CreateFromConnectionString(connectionString, ServiceBusTopics.Email, ServiceBusSubscriptions.AllMessages);

            // Configure the callback options
            var options = new OnMessageOptions
            {
                AutoComplete = false
            };

            Console.WriteLine("Press any key to exit.\n\n");

            subscriptionClient.OnMessage(message =>
            {
                try
                {
                    // Process message from subscription
                    Console.WriteLine("\n**Email Messages**");
                    Console.WriteLine("Body: " + message.GetBody<string>());
                    Console.WriteLine("MessageID: " + message.MessageId);
                    Console.WriteLine("Email: " + message.Properties["To"]);

                    // Remove message from subscription
                    message.Complete();
                }
                catch (Exception)
                {
                    // Indicates a problem, unlock message in subscription
                    message.Abandon();
                }
            }, options);

            
            Console.ReadKey();
        }
    }
}