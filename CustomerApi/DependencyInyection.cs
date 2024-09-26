using BlazorStateManagement.Api.Data;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorStateManagement.Api
{
    public static class DependencyInyection
    {
        public static IServiceCollection AddWebApi(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseInMemoryDatabase("CustomerDb");
            });

            services.AddMediatR(o =>
            {
                o.RegisterServicesFromAssembly(typeof(DependencyInyection).Assembly);
            });

            return services;
        }
    }
}
