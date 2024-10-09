using FlashCard.Auth.Options;
using FlashCard.Auth.Requirements;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCard.Auth
{
    public class FLCAuthorizationPolicyProvider : IAuthorizationPolicyProvider
    {
        private static readonly AuthorizationPolicy? _defaultPolicy = CreateDefaultPolicy();
        private readonly string _scope;
        public FLCAuthorizationPolicyProvider(FLCAuthenticationOptions options)
        {
            _scope = options.Scope;
        }
        public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
        {
            if (_defaultPolicy is null)
                throw new NullReferenceException(nameof(_defaultPolicy));

            return Task.FromResult(_defaultPolicy);
        }

        /// <summary>
        /// Get Fallback Policy
        /// </summary>
        /// <returns></returns>
        public Task<AuthorizationPolicy?> GetFallbackPolicyAsync()
        {
            //return Task.FromResult(_defaultPolicy);
            AuthorizationPolicyBuilder? policyBuilder = new();

            policyBuilder
                .AddRequirements(new AppVersionRequirement())
                .AddRequirements(new PermissionRequirement(PermissionRequirement.Swagger));

            return Task.FromResult(policyBuilder?.Build());
        }

        /// <summary>
        /// Get policy 
        /// </summary>
        /// <param name="policyName"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {
            AuthorizationPolicy? authorizationPolicy = policyName switch
            {
                "UserRegistrationAuthorize" => CreateUserRegistrationPolicy(policyName),
                "AnonymousAuthorize" => CreateAllowAnonymousPolicy(policyName),
                _ => null
            };

            return Task.FromResult(authorizationPolicy);
        }

        private static AuthorizationPolicy? CreateUserRegistrationPolicy(string policyName)
        {
            IAuthorizationRequirement? requirement = new PermissionRequirement(policyName);

            AuthorizationPolicyBuilder? builder = new();

            return builder
                .AddRequirements(requirement)
                .AddRequirements(new AppVersionRequirement())
                .RequireAuthenticatedUser()
                .Build();
        }

        private static AuthorizationPolicy? CreateAllowAnonymousPolicy(string policyName)
        {
            AuthorizationPolicyBuilder? builder = new();

            return builder
                .AddRequirements(new AppVersionRequirement())
                .Build();
        }
        private static AuthorizationPolicy CreateDefaultPolicy()
        {
            AuthorizationPolicyBuilder policyBuilder = new();

            return policyBuilder
                .AddRequirements(new AppVersionRequirement())
                .RequireAuthenticatedUser()
                .Build();
        }
    }
}
