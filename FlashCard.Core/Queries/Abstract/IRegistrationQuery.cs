using FlashCard.Core.Entities;
using FlashCard.Core.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCard.Core.Queries.Abstract
{
    public interface IRegistrationQuery : IQuery<Registration>
    {
        IRegistrationQuery FilterByPhoneNumber(string mobileNo);
        IRegistrationQuery FilterActive(bool isActive);
    }
}
