using AssemblyLine.Common.Constants;
using Microsoft.ServiceBus;

namespace AssemblyLine.Common.Initializers
{
    public class ServiceBusEmailInitializer : IInitializable
    {
        private readonly string _connectionString;

        public ServiceBusEmailInitializer(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Initialize()
        {
            NamespaceManager namespaceManager = NamespaceManager.CreateFromConnectionString(_connectionString);
            if (!namespaceManager.TopicExists(ServiceBusTopics.Email))
            {
                namespaceManager.CreateTopic(ServiceBusTopics.Email);
            }
            if (!namespaceManager.SubscriptionExists(ServiceBusTopics.Email, ServiceBusSubscriptions.AllMessages))
            {
                namespaceManager.CreateSubscription(ServiceBusTopics.Email, ServiceBusSubscriptions.AllMessages);
            }
        }
    }
}