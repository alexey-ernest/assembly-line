using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using AssemblyLine.Common.Exceptions;
using AssemblyLine.Common.Logging;
using Microsoft.Practices.Unity;

namespace AssemblyLine.Infrastructure.Filters.Api
{
    /// <summary>
    ///     Exception handling for Api controllers.
    /// </summary>
    public class HttpExceptionHandlingAttribute : ExceptionFilterAttribute
    {
        [Dependency]
        public ILogService LogService { get; set; }

        public override void OnException(HttpActionExecutedContext context)
        {
            Exception exception = context.Exception;

            // Skip cancelled tasks exceptions
            if (exception is OperationCanceledException)
            {
                return;
            }


            // Handle special exceptions
            Guid correlationToken = Guid.NewGuid();
            if (exception is BadRequestException)
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            else if (exception is UnauthorizedException)
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }
            else if (exception is ForbiddenException)
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
            }
            else if (exception is NotFoundException)
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            else if (exception is ConflictException)
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.Conflict);
            }
            else if (exception is NotImplementedException)
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.NotImplemented);
            }
            else if (exception is BadGatewayException)
            {
                // 502

                // logging
                LogService.WriteAsync(exception, correlationToken);

                // building response
                context.Response = new HttpResponseMessage(HttpStatusCode.BadGateway)
                {
                    Content =
                        new StringContent(
                            string.Format(
                                "Service Error. Ticket - {0}.",
                                correlationToken)),
                    ReasonPhrase = "Error"
                };
            }
            else
            {
                // 500

                // logging
                LogService.WriteAsync(exception, correlationToken);

                // building response
                context.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content =
                        new StringContent(
                            string.Format(
                                "Server Error. Ticket - {0}.",
                                correlationToken)),
                    ReasonPhrase = "Error"
                };
            }
        }
    }
}