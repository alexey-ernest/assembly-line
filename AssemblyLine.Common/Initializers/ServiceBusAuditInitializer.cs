using AssemblyLine.Common.Constants;
using Microsoft.ServiceBus;

namespace AssemblyLine.Common.Initializers
{
    public class ServiceBusAuditInitializer : IInitializable
    {
        private readonly string _connectionString;

        public ServiceBusAuditInitializer(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Initialize()
        {
            NamespaceManager namespaceManager = NamespaceManager.CreateFromConnectionString(_connectionString);
            if (!namespaceManager.TopicExists(ServiceBusTopics.Audit))
            {
                namespaceManager.CreateTopic(ServiceBusTopics.Audit);
            }
            if (!namespaceManager.SubscriptionExists(ServiceBusTopics.Audit, ServiceBusSubscriptions.AllMessages))
            {
                namespaceManager.CreateSubscription(ServiceBusTopics.Audit, ServiceBusSubscriptions.AllMessages);
            }
        }
    }
}