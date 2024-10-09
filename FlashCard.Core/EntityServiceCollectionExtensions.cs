using FlashCard.Abstract.Request;
using FlashCard.Core.Repositories;
using FlashCard.Core.Repositories.Abstract;
using FlashCard.Core.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FlashCard.Core
{
    public static class EntityServiceCollectionExtensions
    {
        public static IServiceCollection AddEntityServices(this IServiceCollection services, IConfiguration configuration)
        {
            string? connectionString = configuration.GetConnectionString(EntityConstants.DatabaseSection);
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new NullReferenceException(nameof(connectionString));
            }
            return services
                .AddDbContext<FlashCardDbContext>(options => options.UseSqlServer(connectionString, options => options.EnableRetryOnFailure()))
                .AddDbContextFactory<FlashCardDbContext>(options => options.UseSqlServer(connectionString, options => options.EnableRetryOnFailure()), ServiceLifetime.Scoped)
                .AddScoped<IUnitOfWorkService, UnitOfWorkService>()
                .AddScoped<ICardRepository, CardRepository>();
        }
    }
}
