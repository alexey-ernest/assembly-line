using AssemblyLine.Common.Constants;
using Microsoft.ServiceBus;

namespace AssemblyLine.Common.Initializers
{
    public class ServiceBusOutlookInitializer : IInitializable
    {
        private readonly string _connectionString;

        public ServiceBusOutlookInitializer(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Initialize()
        {
            NamespaceManager namespaceManager = NamespaceManager.CreateFromConnectionString(_connectionString);
            if (!namespaceManager.TopicExists(ServiceBusTopics.Outlook))
            {
                namespaceManager.CreateTopic(ServiceBusTopics.Outlook);
            }
            if (!namespaceManager.SubscriptionExists(ServiceBusTopics.Outlook, ServiceBusSubscriptions.AllMessages))
            {
                namespaceManager.CreateSubscription(ServiceBusTopics.Outlook, ServiceBusSubscriptions.AllMessages);
            }
        }
    }
}