using FlashCard.API.Models.Validations;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using System.Net;

namespace FlashCard.API.Middlewares
{
    public class CustomAuthorizationMiddlewareResultHandler : IAuthorizationMiddlewareResultHandler
    {
        private readonly AuthorizationMiddlewareResultHandler
             DefaultHandler = new AuthorizationMiddlewareResultHandler();

        public async Task HandleAsync(
            RequestDelegate requestDelegate,
            HttpContext httpContext,
            AuthorizationPolicy authorizationPolicy,
            PolicyAuthorizationResult policyAuthorizationResult)
        {
            AuthorizationFailureReason? failureReason = policyAuthorizationResult?.AuthorizationFailure?.FailureReasons.FirstOrDefault();

            if
            (
                failureReason is not null
            )
            {
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                await httpContext.Response
                    .WriteAsync(JsonConvert.SerializeObject(ErrorDetail.CreateErrorDetail(failureReason)));

                return;
            }

            // Fallback to the default implementation.
            await DefaultHandler.HandleAsync(requestDelegate, httpContext, authorizationPolicy,
                                   policyAuthorizationResult);
        }
    }
}
