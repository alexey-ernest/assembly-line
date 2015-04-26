using System.Net.Http.Formatting;
using System.Web.Http;
using Newtonsoft.Json.Serialization;

namespace AssemblyLine
{
    public static class JsonConfig
    {
        public static void Configure(HttpConfiguration configuration)
        {
            JsonMediaTypeFormatter json = configuration.Formatters.JsonFormatter;
            json.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}