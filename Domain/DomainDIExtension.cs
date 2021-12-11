using Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Domain
{
    public static class DomainDIExtension
    {
        public static void RegisterDomain(this IServiceCollection collection)
        {
            collection.AddScoped<IUserDomainService, UserDomainService>();
        }
    }
}