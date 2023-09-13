using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using TaskManagement.Api.Controllers;
using TaskManagement.Core.Implementation;
using TaskManagement.Core.Services;
using TaskManagement.Data.AppDbContext;

namespace TaskManagement.Api.Extensions
{
    public static class DbRegistryExtension
    {
        public static void AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
           //Register your DbContext with the DI container
            services.AddDbContext<TaskManagementDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("conn"));
            });

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<IOtherRepository, OtherRepository>();
            services.AddScoped<NotificationService>();
          
        }
    }
}
