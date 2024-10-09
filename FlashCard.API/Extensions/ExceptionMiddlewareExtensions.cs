using FlashCard.Abstract.Exceptions;
using FlashCard.API.Models.Validations;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System.Net;

namespace FlashCard.API.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        private const string ErrorLogMessageFormat = "Something went wrong: {0}";
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILogger logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature is null)
                        return;

                    if (contextFeature.Error is PermissionDeniedException perException)
                    {
                        context.Response.ContentType = "application/json";
                        context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                        await context.Response
                            .WriteAsync(JsonConvert.SerializeObject
                                (new ErrorDetail((int)HttpStatusCode.Forbidden, perException.Message)));

                        return;
                    }

                    string? referenceId = string.Empty;
                    if (context?.Request.Headers.TryGetValue(ApiConstants.HeaderReferenceIDKey, out StringValues value) ?? false)
                    {
                        referenceId = value.FirstOrDefault();
                    }

                    string errorMessage = string.IsNullOrEmpty(referenceId) ?
                        string.Format(ErrorLogMessageFormat, contextFeature.Error.Message)
                        : $"RequestId: {referenceId} - {string.Format(ErrorLogMessageFormat, contextFeature.Error.Message)}";

                    logger.LogError(errorMessage);
                    await context.Response
                        .WriteAsync(JsonConvert.SerializeObject(new ErrorDetail(context.Response.StatusCode, errorMessage)));
                });
            });
        }
    }
}
