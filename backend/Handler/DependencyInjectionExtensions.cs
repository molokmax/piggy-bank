using Handler.Movement;
using Handler.MovementHistory;
using Handler.Purpose;
using Handler.Storage;
using Handler.StoreItem;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Handler
{
    public static class DependencyInjectionExtensions
    {
        public static void AddHandlers(
            this IServiceCollection services, IConfiguration configuration)
        {
            // TODO: register app handler automatically
            services.AddTransient<CreatePurposeHandler>();
            services.AddTransient<UpdatePurposeHandler>();
            services.AddTransient<DeletePurposeHandler>();
            services.AddTransient<ReadPurposeHandler>();
            services.AddTransient<CreateStorageHandler>();
            services.AddTransient<UpdateStorageHandler>();
            services.AddTransient<DeleteStorageHandler>();
            services.AddTransient<ReadStorageHandler>();
            services.AddTransient<ReadStoreItemHandler>();
            services.AddTransient<ReadMovementHistoryHandler>();
            services.AddTransient<CreateMovementHandler>();
        }

    }
}
