using FlashCard.Business.Cards;
using FlashCard.Business.MemoryCaches;
using Microsoft.Extensions.DependencyInjection;

namespace FlashCard.Business
{
    public static class BusinessServiceCollectionExtensions
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {

            return services
                .AddTransient<IFlashCardCacheService, FlashCardCacheService>()
                .AddTransient<ICardServices, CardServices>();
        }
    }
}
