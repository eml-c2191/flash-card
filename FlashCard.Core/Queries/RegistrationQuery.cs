using FlashCard.Core.Entities;
using FlashCard.Core.Queries.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCard.Core.Queries
{
    public class RegistrationQuery : BaseQuery<Registration, int>, IRegistrationQuery
    {
        public RegistrationQuery(IQueryable<Registration> query) : base(query)
        {

        }
        public IRegistrationQuery FilterActive(bool isActive)
        {
            Query = Query.Where(registration => registration.IsActive == isActive);

            return this;
        }
        public IRegistrationQuery FilterByPhoneNumber(string mobileNo)
        {
            Query = Query.Where(registration => registration.MobileNo == mobileNo);

            return this;
        }
    }
}
