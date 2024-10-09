using FlashCard.Abstract.Exceptions;
using FlashCard.Abstract.Extensions;
using FlashCard.Abstract.Responses;
using FlashCard.API.Models.ClientRequests;
using FlashCard.API.Models.Options;
using FlashCard.API.Services.Abstractions;
using FlashCard.Business.MemoryCaches;
using Microsoft.Extensions.Options;
using System.Web;

namespace FlashCard.API.Services
{
    public class IdentityClient : IIdentityClient
    {
        private readonly HttpClient _httpClient;
        private readonly IFlashCardCacheService _cacheService;

        private int limitOTPPerdayCount;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="options"></param>
        /// <param name="controlServices"></param>
        /// <param name="cacheService"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public IdentityClient
        (
            HttpClient httpClient,
            IOptionsMonitor<IdentityClientOptions> options,
            IFlashCardCacheService cacheService
        )
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = options?.CurrentValue?.Endpoint ?? throw new ArgumentNullException(nameof(options));
            limitOTPPerdayCount = options?.CurrentValue?.OTPRequestLimitCount ?? 0;
            _cacheService = cacheService;
        }

        public async Task RequestOtpAsync(string mobileNo)
        {
            string otpCacheEntry = $"{CacheEntries.OTPCodeEntry}{DateTime.Now.Date.ToString("yyyyMMdd")}{mobileNo}";
            int? count = _cacheService.Get<int>(otpCacheEntry);
            if (count.HasValue && count >= limitOTPPerdayCount)
                throw new BusinessValidationException(ApiConstants.ExceededOTPRequestLimit);

            count ??= 0;
            count++;
            _cacheService.Set(otpCacheEntry, count, 24 * 60 * 60);
            //TODO: need to check if the OTPconfig is enabled
            string route = $"otps/request?mobileNo={HttpUtility.UrlEncode(mobileNo)}";
            HttpResponseMessage message = await _httpClient.GetAsync(route);

            if (message.IsSuccessStatusCode)
                return;

            throw new BusinessValidationException(ApiConstants.InvalidMobilePhone);
        }
        public async Task<bool> VerifyOtpAsync(ClientVerifyOtpRequest verifyOtpRequest)
        {   
            //TODO: need to check if the OTPconfig is enabled
            HttpResponseMessage? response = await _httpClient
                .PostAsync("otps/verify", verifyOtpRequest.ConvertToStringContent());

            return response?.IsSuccessStatusCode ?? false;
        }

        public async Task<AuthoriseResponse> GetTokenAsync(IEnumerable<KeyValuePair<string, string>> claims)
        {
            AuthoriseResponse? response = await _httpClient
               .PostWithObjectReturnAsync<AuthoriseResponse, GetTokenRequest>("authorises/token", new GetTokenRequest
               {
                   Claims = claims
               });

            return response ?? throw new BusinessValidationException();
        }
    }
}
