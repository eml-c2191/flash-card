using FlashCard.Core.Entities;
using FlashCard.Core.Queries.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCard.Core.Repositories.Abstract
{
    public interface IRegistrationRepository : IRepository<Registration, int>
    {
        public IRegistrationQuery BuildQuery();
    }
}
