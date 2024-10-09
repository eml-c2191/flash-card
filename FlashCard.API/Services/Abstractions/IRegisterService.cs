using System.Runtime.InteropServices;

namespace FlashCard.API.Services.Abstractions
{
    public interface IRegisterService
    {
        Task<(int registrationId, string registrationHash)> RegisterAsync(string mobileNo);
        Task<int?> GetRegistrationIdAsync(string mobileNo);
        Task<bool> CheckActiveAsync(int registrationId);
        Task<(bool isActive, int? registrationId)> CheckActiveAsync(string mobileNumber, string registrationHash);
        Task UnRegisterAsync(int registrationId);
    }
}
