//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using RedAcademy_task_2.Areas.Identity.Data;

//namespace MyWebAppIdentity.Configurations
//{
//    public static class ContextConfig
//    {
//        public static IServiceCollection AddContextConfig(this IServiceCollection services, IConfiguration configuration)
//        {            
 
//            services.AddDbContext<AppIdentityContext>(options =>
//            options.UseSqlServer(configuration.GetConnectionString("WebApplication1ContextConnection")));

//            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//                .AddRoles<IdentityRole>()
//                .AddEntityFrameworkStores<AppIdentityContext>();

//            services.AddDbContext<AppIdentityContext>(options =>
//            options.UseSqlServer(configuration.GetConnectionString("MyWebAppIdentityContextConnection")));
//            return services;
//        }
//        public static void UseIdentityConfiguration(this IApplicationBuilder app)
//        {
//            app.UseAuthentication();
//            app.UseAuthorization();
//        }
//    }
//}