using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TimeToGo2.WebApp.Data;
namespace TimeToGo2.WebApp
{
    using System.Reflection;
    using MediatR;
    using TimeToGo2.WebApp.Configuration;
    using TimeToGo2.WebApp.Features.Weather;
    using TimeToGo2.WebApp.ViewModels;
    using DevExpress.Blazor;
    using TimeToGo2.WebApp.Areas.Identity;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("TimeToGoDbContextConnection") ?? throw new InvalidOperationException("Connection string 'AppContextConnection' not found.");

            builder.Services.AddDbContext<TimeToGoDbContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                   .AddRoles<IdentityRole>()
                   .AddEntityFrameworkStores<TimeToGoDbContext>();

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
            builder.Services.AddSingleton<IJobConstraints, JobConstraints>();
            builder.Services.AddDevExpressBlazor(configure => configure.BootstrapVersion = BootstrapVersion.v5);
            builder.Services.AddSingleton<MonthPageViewModel>();
            
            builder.Services.AddScoped<TokenProvider>();
            builder.WebHost.UseWebRoot("wwwroot");
            builder.WebHost.UseStaticWebAssets();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();
            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}