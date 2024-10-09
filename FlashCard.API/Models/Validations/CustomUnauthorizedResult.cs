using Microsoft.AspNetCore.Mvc;

namespace FlashCard.API.Models.Validations
{
    public class CustomUnauthorizedResult : JsonResult
    {
        public CustomUnauthorizedResult(string message)
           : base(new CustomError(message))
        {
            StatusCode = StatusCodes.Status401Unauthorized;
        }
    }
    public class CustomError
    {
        public string Message { get; }

        public CustomError(string message)
        {
            Message = message;
        }
    }
}
