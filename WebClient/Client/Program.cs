using Blazored.LocalStorage;
using DataLayer.Entities;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WebClient.Handlers;
using WebClient.Services.Implementations;
using WebClient.Services.Interfaces;

namespace WebClient
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddTransient<AuthenticationHandler>();

            builder.Services.AddHttpClient("ServerApi")
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration["ServerUrl"] ?? ""))
                .AddHttpMessageHandler<AuthenticationHandler>()
                ;

            builder.Services.AddSingleton<IAccountService, AccountService>();
            builder.Services.AddTransient<IRequestService, RequestService>();
            builder.Services.AddTransient<IService<Project>, ProjectService>();
            builder.Services.AddTransient<IService<BlogArticle>, BlogArticleService>();
            builder.Services.AddTransient<IService<Service>, ServiceService>();

            builder.Services.AddBlazoredLocalStorageAsSingleton();

            await builder.Build().RunAsync();

        }
    }
}