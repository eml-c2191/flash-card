using Microsoft.EntityFrameworkCore;

namespace FlashCard.Core.DBMappings
{
    public static class DbMapping
    {
        public static void ApplyAllConfiguration(this ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CardConfiguration());
        }
    }
}
