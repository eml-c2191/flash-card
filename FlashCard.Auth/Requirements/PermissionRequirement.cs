using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCard.Auth.Requirements
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public string Permission { get; set; }
        public static string Swagger = "Swagger";


        public PermissionRequirement(string permission)
        {
            Permission = permission;
        }
    }
}
