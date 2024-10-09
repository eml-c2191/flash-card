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
         

            services.AddAuthentication(AuthorizationConstants.UserRegistrationScheme)
                .AddJwtBearer(AuthorizationConstants.UserRegistrationScheme, options =>
                {
                    //TODO: implement validate signing key
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = flcAuthenticationOptions.AccessTokenIssuer,
                        ValidAudience = flcAuthenticationOptions.AccessTokenAudience,
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
    }
}
