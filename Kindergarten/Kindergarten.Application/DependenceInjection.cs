using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Kindergarten.Application
{
    public static class DependenceInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(typeof(DependenceInjection).Assembly);

            return services;
        }
    }
}
