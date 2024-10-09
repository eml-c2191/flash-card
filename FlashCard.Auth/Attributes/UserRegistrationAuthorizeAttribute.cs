using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCard.Auth.Attributes
{
    public sealed class UserRegistrationAuthorizeAttribute : AuthorizeAttribute
    {
        public static string PolicyName = "UserRegistrationAuthorize";
        public UserRegistrationAuthorizeAttribute() : base(PolicyName)
        {
            AuthenticationSchemes = AuthorizationConstants.UserRegistrationScheme;
        }
    }
}
