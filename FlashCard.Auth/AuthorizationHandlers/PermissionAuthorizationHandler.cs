using FlashCard.Abstract.UserContext;
using FlashCard.Auth.Requirements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FlashCard.Auth.AuthorizationHandlers
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IUserContext _userContext;
        private readonly IHttpContextAccessor _httpContextAccessor;


        /// <summary>
        /// Constructor
        /// </summary>
        public PermissionAuthorizationHandler
        (
            IUserContext userContext,
            IHttpContextAccessor httpContextAccessor
        )
        {
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

            //bool isActive = await _registrationService.CheckActiveAsync(_userContext.RegistrationId.Value);

            //if (!isActive)
            //{
            //    context.Fail();
            //    return;
            //}

            if (!_userContext.RegistrationId.HasValue)
            {
                context.Fail();
                return;
            }

            context.Succeed(requirement);
        }
    }
}
