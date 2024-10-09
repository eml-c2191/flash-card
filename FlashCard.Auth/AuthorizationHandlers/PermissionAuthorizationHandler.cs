using FlashCard.Abstract.UserContext;
using FlashCard.Auth.Requirements;
using FlashCard.Business.Registrations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace FlashCard.Auth.AuthorizationHandlers
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IUserContext _userContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRegistrationService _registrationService;

        /// <summary>
        /// Constructor
        /// </summary>
        public PermissionAuthorizationHandler
        (
            IRegistrationService registrationService,
            IUserContext userContext,
            IHttpContextAccessor httpContextAccessor
        )
        {
            _registrationService = registrationService;
            _userContext = userContext;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Handle authentication
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        protected override async Task HandleRequirementAsync
        (
            AuthorizationHandlerContext context,
            PermissionRequirement requirement
        )
        {
            if (requirement.Permission == PermissionRequirement.Swagger)
            {
                context.Succeed(requirement);
                return;
            }

            if (!_userContext.RegistrationId.HasValue)
            {
                context.Fail();
                return;
            }

            bool isActive = await _registrationService.CheckActiveAsync(_userContext.RegistrationId.Value);

            if (!isActive)
            {
                context.Fail();
                return;
            }

            if (!_userContext.RegistrationId.HasValue)
            {
                context.Fail();
                return;
            }

            context.Succeed(requirement);
        }
    }
}
