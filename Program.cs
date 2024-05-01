using Hangfire;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using Hangfire.Dashboard;
using Hangfire.EntityFrameworkCore;

namespace MVC_CORE
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"));

            builder.Services.AddControllersWithViews(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                options.Filters.Add(new AuthorizeFilter(policy));

            });
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                                   ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services.AddDbContext<ExampleDbContext>();

            builder.Services.AddHangfire(configuration =>
                configuration.UseEFCoreStorage(dbContextOptionsBuilder =>
                            dbContextOptionsBuilder.UseSqlite(connectionString),
                        new EFCoreStorageOptions
                        {
                            CountersAggregationInterval = new TimeSpan(0, 5, 0),
                            DistributedLockTimeout = new TimeSpan(0, 10, 0),
                            JobExpirationCheckInterval = new TimeSpan(0, 30, 0),
                            QueuePollInterval = new TimeSpan(0, 0, 15),
                            Schema = string.Empty,
                            SlidingInvisibilityTimeout = new TimeSpan(0, 5, 0),
                        }).
                    UseDatabaseCreator());

            builder.Services.AddRazorPages()
                .AddMicrosoftIdentityUI();

            var app = builder.Build();

            app.UseHangfireDashboard(
                "/hangfire",
                new DashboardOptions
                {
                    AppPath = "https://localhost:44378/",
                    Authorization = Array.Empty<IDashboardAuthorizationFilter>(),
                    DarkModeEnabled = true,
                    DashboardTitle = "Hangfire BCT Dashboard",
                    DisplayStorageConnectionString = true,
                    DefaultRecordsPerPage = 10
                });

            // app.UseMiddleware<ModelStateValidationMiddleware>();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //app.UseAuthorization();
           // app.UseAuthentication();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller}/{action}/{id?}",
                defaults: new { controller = "Home", action = "Index" });
            app.MapRazorPages();

            app.Run();
        }
    }
}
