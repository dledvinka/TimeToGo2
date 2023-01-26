namespace TimeToGo2.WebApp
{
    using System.Reflection;
    using MediatR;
    using TimeToGo2.WebApp.Configuration;
    using TimeToGo2.WebApp.Features.Weather;
    using TimeToGo2.WebApp.ViewModels;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
            builder.Services.AddSingleton<IJobConstraints, JobConstraints>();
            builder.Services.AddSingleton<MonthPageViewModel>();

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

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}