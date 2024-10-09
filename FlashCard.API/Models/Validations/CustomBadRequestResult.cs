using Microsoft.AspNetCore.Mvc;

namespace FlashCard.API.Models.Validations
{
    public class CustomBadRequestResult : JsonResult
    {
        private static string InvalidRequest = "Invalid request";

        public CustomBadRequestResult(string message)
            : base(new CustomError(message))
        {
            StatusCode = StatusCodes.Status400BadRequest;
        }

        public CustomBadRequestResult(string message, IDictionary<string, string> errors)
            : base(new ValidationResultModel(message, errors))
        {
            StatusCode = StatusCodes.Status400BadRequest;
        }

        public static CustomBadRequestResult Fail(string? message = null)
        {
            return new CustomBadRequestResult(message ?? InvalidRequest);
        }

        public static CustomBadRequestResult Fail(string message, IDictionary<string, string> errors)
        {
            return new CustomBadRequestResult(message, errors);
        }
    }
}
