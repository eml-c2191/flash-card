using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCard.Abstract.UserContext
{
    public interface IUserContext
    {
        string Identity { get; }
        int? RegistrationId { get; }
        DateTime? DateOrBirth { get; }

        public string? Token { get; }
    }
}
