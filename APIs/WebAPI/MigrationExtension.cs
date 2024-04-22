using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace WebAPI
{
    public static class MigrationExtension
    {
        public static void ApplyMigration(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();
            using AppDbContext appDbContext = scope.ServiceProvider.GetService<AppDbContext>();
            if (appDbContext != null)
            {
                appDbContext.Database.Migrate();
            }
        }
    }
}
