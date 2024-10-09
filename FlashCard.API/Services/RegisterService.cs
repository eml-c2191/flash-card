using FlashCard.API.Services.Abstractions;
using FlashCard.Business.Registrations;
using System.Runtime.InteropServices;

namespace FlashCard.API.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly IRegistrationService _registrationService;
        public RegisterService(IRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }
        public async Task<(int registrationId, string registrationHash)> RegisterAsync(
          string mobileNo)
        {
            // De-active old registration
            int? oldRegistrationId = await _registrationService.GetRegistrationIdAsync(mobileNo);
            if (oldRegistrationId.HasValue)
            {
                await UnRegisterAsync(oldRegistrationId.Value);
            }
            // Create registration
            (int registrationId, string registrationHash) = await _registrationService.RegisterAsync(mobileNo);

            return (registrationId, registrationHash);
        }
        public async Task UnRegisterAsync(int registrationId)
        {
            await _registrationService.UnRegisterAsync(registrationId);
        }
    }
}
