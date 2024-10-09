using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCard.Auth.Attributes
{
    public sealed class AnonymousAuthorizeAttribute : AuthorizeAttribute
    {
        private static string PolicyName = "AnonymousAuthorize";
        public AnonymousAuthorizeAttribute() : base(PolicyName)
        {
            AuthenticationSchemes = AuthorizationConstants.AnonymousScheme;
        }
    }
}
