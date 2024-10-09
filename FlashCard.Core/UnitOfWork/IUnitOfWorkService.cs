using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCard.Core.UnitOfWork
{
    public interface IUnitOfWorkService
    {
        Task SaveChangeAsync();
    }
}
