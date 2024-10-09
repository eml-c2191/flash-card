using FlashCard.Abstract.Responses;
using FlashCard.API.Models.ClientRequests;

namespace FlashCard.API.Services.Abstractions
{
    public interface IIdentityClient
    {
        Task RequestOtpAsync(string mobileNo);
        Task<bool> VerifyOtpAsync(ClientVerifyOtpRequest verifyOtpRequest);

        Task<AuthoriseResponse> GetTokenAsync(IEnumerable<KeyValuePair<string, string>> claims);
    }
}
