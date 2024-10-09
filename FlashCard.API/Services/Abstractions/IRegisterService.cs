using System.Runtime.InteropServices;

namespace FlashCard.API.Services.Abstractions
{
    public interface IRegisterService
    {
        Task<(int registrationId, string registrationHash)> RegisterAsync(string mobileNo);
        Task UnRegisterAsync(int registrationId);
    }
}
