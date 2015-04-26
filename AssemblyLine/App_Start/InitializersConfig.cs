using System;
using System.Collections.Generic;
using AssemblyLine.Common.Initializers;
using AssemblyLine.Common.Logging;
using Microsoft.Practices.Unity;

namespace AssemblyLine
{
    public class InitializersConfig
    {
        public static void Register(IUnityContainer container)
        {
            var initializers = new List<IInitializable>
            {
                container.Resolve<ServiceBusEmailInitializer>(),
                container.Resolve<ServiceBusAuditInitializer>(),
                container.Resolve<ServiceBusOutlookInitializer>()
            };

            try
            {
                foreach (IInitializable initializer in initializers)
                {
                    initializer.Initialize();
                }
            }
            catch (Exception e)
            {
                var log = container.Resolve<ILogService>();
                log.WriteAsync(e).Wait();
            }
        }
    }
}