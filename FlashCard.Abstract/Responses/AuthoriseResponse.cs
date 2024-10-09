using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCard.Abstract.Responses
{
    public record AuthoriseResponse
    {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
    public record VerifyOtpResponse : AuthoriseResponse
    {
        public VerifyOtpResponse(AuthoriseResponse response)
        {
            AccessToken = response.AccessToken;
            RefreshToken = response.RefreshToken;
        }
    }
}
