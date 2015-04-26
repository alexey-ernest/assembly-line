using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using Microsoft.Data.OData;

namespace AssemblyLine.Infrastructure.Filters.Api
{
    public sealed class ODataValidationExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            // Handle OData exceptions caused by empty or null values in query parameters
            if (context.Exception is ODataException || context.Exception is ArgumentException)
            {
                context.Response = context.Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    context.Exception.Message);
            }
        }
    }
}