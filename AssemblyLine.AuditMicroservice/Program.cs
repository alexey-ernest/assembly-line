using AssemblyLine.Common.Configuration;
using AssemblyLine.Common.Constants;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;

namespace AssemblyLine.AuditMicroservice
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
            TopicClient subscriptionClient = TopicClient.CreateFromConnectionString(connectionString, ServiceBusTopics.Email);

            // Send message
            BrokeredMessage message = new BrokeredMessage("Test email message");
            message.Properties["To"] = "alexey.ernest@gmail.com";
            subscriptionClient.Send(message);
        }
    }
}