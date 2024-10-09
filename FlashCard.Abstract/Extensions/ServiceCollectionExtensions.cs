using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCard.Abstract.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Get Options
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static void AddFLCOptions<T>(this IServiceCollection services) where T : class
        {
            services.AddOptions<T>()
                .BindConfiguration(typeof(T).Name)
                .ValidateDataAnnotations()
                .ValidateOnStart();
        }

        /// <summary>
        /// Get Options
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <param name="data"></param>
        public static void AddFLCOptions<T>
        (
            this IServiceCollection services,
            T data
        ) where T : class
        {
            services.AddOptions<T>()
                .BindConfiguration(typeof(T).Name)
                .ValidateDataAnnotations()
                .ValidateOnStart();
        }
    }
}
