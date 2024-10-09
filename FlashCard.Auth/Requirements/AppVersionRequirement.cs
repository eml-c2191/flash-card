using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCard.Auth.Requirements
{
    public class AppVersionRequirement : IAuthorizationRequirement
    {
        public AppVersionRequirement()
        { }
    }
}
