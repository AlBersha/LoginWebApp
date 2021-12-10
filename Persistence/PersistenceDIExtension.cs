using System;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Persistence
{
    public static class PersistenceDIExtension
    {
        public static void RegisterPersistence(this IServiceCollection collection, IConfiguration configuration)
        {
            collection.AddDbContext<ApplicationContext>(
                builder => builder.UseSqlServer(
                    "Server=5CG914068X;Database=LoginAppDatabase;Integrated Security=True;", builder =>
                    {
                        builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                    }));
            collection.AddTransient<IUserRepository, UserRepository>();
        }
    }
}