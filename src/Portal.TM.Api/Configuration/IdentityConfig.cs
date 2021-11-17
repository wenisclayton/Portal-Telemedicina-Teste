using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Portal.TM.Business.Entities;
using Portal.TM.Data.Context;

namespace Portal.TM.Api.Configuration
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services,
            IConfiguration config)
        {

            //services.AddEntityFrameworkInMemoryDatabase()
            //    .AddDbContext<MyIdentityDbContext>(o => o.UseInMemoryDatabase("portal-tm"));

            services.AddDbContext<MyIdentityDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection")));


            services.AddDefaultIdentity<MyUser>()
                .AddEntityFrameworkStores<MyIdentityDbContext>()
                .AddDefaultTokenProviders();


            return services;
        }
    }
}
