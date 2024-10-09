using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCard.Abstract.UserContext
{
    public class UserContext : IUserContext
    {
        public UserContext([NotNull] string identity, DateTime? dateOfBirth, int? registrationId, string? token)
        {
            Identity = identity;
            DateOrBirth = dateOfBirth;
            RegistrationId = registrationId;
            Token = token;
        }

        public string Identity { get; }


        public DateTime? DateOrBirth { get; }


        public int? RegistrationId { get; }


        public string? Token { get; }
    }
}
