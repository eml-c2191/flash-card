using FlashCard.Abstract.UserContext;
using FlashCard.Core.Entities;
using FlashCard.Core.Queries.Abstract;
using FlashCard.Core.Queries;
using FlashCard.Core.Repositories.Abstract;

namespace FlashCard.Core.Repositories
{
    public class RegistrationRepository :BaseRepository<Registration, int>, IRegistrationRepository
    {
        public RegistrationRepository
        (
          FlashCardDbContext context,
          IUserContext userContext
        ) : base(context, userContext)
        { }
        public IRegistrationQuery BuildQuery()
        {
            return new RegistrationQuery(_context.Registrations);
        }
    }
}
