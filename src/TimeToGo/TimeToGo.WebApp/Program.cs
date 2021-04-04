using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace TimeToGo.WebApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddOidcAuthentication(options =>
            {
                //builder.Configuration.Bind("OidcCinfiguration", options.ProviderOptions);
                options.ProviderOptions.Authority = "https://localhost:44333";
                options.ProviderOptions.ClientId = "bethanyspieshophr";
                options.ProviderOptions.RedirectUri = "https://localhost:44341/authentication/login-callback";
                options.ProviderOptions.PostLogoutRedirectUri = "https://localhost:44341/authentication/logout-callback";
                options.ProviderOptions.DefaultScopes.Add("email"); 
                options.ProviderOptions.ResponseType = "code";
            });

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            await builder.Build().RunAsync();
        }
    }
}
