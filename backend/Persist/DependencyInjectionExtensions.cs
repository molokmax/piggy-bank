using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Persist
{
    public static class DependencyInjectionExtensions
    {
        public static void AddDatabaseContext(
            this IServiceCollection services, IConfiguration configuration)
        {
            string connection = configuration.GetConnectionString("DbContext");
            services.TryAddDbContext<DatabaseContext>(options => options.UseSqlServer(connection));
        }

        public static void TryAddDbContext<TContext>(
            this IServiceCollection services,
            Action<DbContextOptionsBuilder> optionsAction) where TContext : DbContext
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (!services.Any(d => d.ServiceType == typeof(TContext)))
            {
                services.AddDbContext<TContext>(optionsAction);
            }
        }
    }
}
