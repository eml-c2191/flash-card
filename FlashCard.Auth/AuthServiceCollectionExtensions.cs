using FlashCard.Abstract;
using FlashCard.Abstract.Extensions;
using FlashCard.Auth.AuthenticationHandlers;
using FlashCard.Auth.AuthorizationHandlers;
using FlashCard.Auth.Options;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
namespace FlashCard.Auth
{
    public static  class AuthServiceCollectionExtensions
    {
        public static IServiceCollection AddAuthServices
       (
           this IServiceCollection services,
           IConfiguration configuration
       )
        {
            services.AddFLCOptions<FLCAuthenticationOptions>();
            IdentityModelEventSource.ShowPII = true;
            FLCAuthenticationOptions flcAuthenticationOptions = configuration.GetSection(nameof(FLCAuthenticationOptions))
                .Get<FLCAuthenticationOptions>() ?? throw new NullReferenceException(nameof(FLCAuthenticationOptions));
            SecurityKey rsaSecurityKey = CreateKey();

            services.AddAuthentication(AuthorizationConstants.UserRegistrationScheme)
                .AddJwtBearer(AuthorizationConstants.UserRegistrationScheme, options =>
                {
                    //TODO: implement validate signing key
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = flcAuthenticationOptions.AccessTokenIssuer,
                        ValidAudience = flcAuthenticationOptions.AccessTokenAudience,
                        IssuerSigningKey = rsaSecurityKey
                    };
                })
                 .AddScheme<AuthenticationSchemeOptions, AnonymousAuthenticationHandler>
                (
                    AuthorizationConstants.AnonymousScheme,
            options => { }
            );
            return services
                .AddSingleton<IAuthorizationPolicyProvider>(new FLCAuthorizationPolicyProvider(flcAuthenticationOptions))
                .AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
        }
        private static SecurityKey CreateKey()
        {
            string path = Directory.GetCurrentDirectory();
            string publicKeyFilePath = Path.Combine(path, "App_Data\\key\\PublicSigning.key");
            if (!File.Exists(publicKeyFilePath))
            {
                throw new Exception("Missing JWT Token Key File");
            }
            RSA publicRsa = RSAHelper.PublicKeyFromPemFile(publicKeyFilePath);
            RsaSecurityKey pub_signingKey = new RsaSecurityKey(publicRsa) { KeyId = "EML.FLC" };
            Console.WriteLine("public key: " + pub_signingKey.ToString());
            return pub_signingKey;
        }
    }
}
