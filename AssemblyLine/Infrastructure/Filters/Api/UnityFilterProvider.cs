using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Microsoft.Practices.Unity;

namespace AssemblyLine.Infrastructure.Filters.Api
{
    public class UnityFilterProvider : ActionDescriptorFilterProvider, IFilterProvider
    {
        private readonly IUnityContainer _container;

        public UnityFilterProvider(IUnityContainer container)
        {
            _container = container;
        }

        public new IEnumerable<FilterInfo> GetFilters(HttpConfiguration configuration,
            HttpActionDescriptor actionDescriptor)
        {
            List<FilterInfo> filters = base.GetFilters(configuration, actionDescriptor).ToList();
            foreach (FilterInfo filter in filters)
            {
                _container.BuildUp(filter.Instance);
            }

            return filters;
        }
    }
}