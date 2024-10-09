using FlashCard.Auth.AuthorizationHandlers;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace FlashCard.API.Models.Validations
{
    public record ErrorDetail
    {
        public ErrorDetail([Required] int statusCode, [NotNull] string message, string? errorCode = null)
        {
            StatusCode = statusCode;
            Message = message;
            ErrorCode = errorCode;
        }

        /// <summary>
        /// Status Code
        /// </summary>
        public int StatusCode { get; set; }

        public string? ErrorCode { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; }

        public static ErrorDetail CreateErrorDetail(AuthorizationFailureReason reason)
        {
            string errorCode = reason.Handler switch
            {
                PermissionAuthorizationHandler permissionAuthorizationHandler => "PermissionFailed" + (string.IsNullOrEmpty(reason.Message) ? string.Empty : $"_{reason.Message}"),
                _ => "UserAuthorizeFailed"
            };

            return new ErrorDetail((int)HttpStatusCode.Forbidden, reason.Message, null);
        }
    }
}
