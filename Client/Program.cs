using Blazored.LocalStorage;
using BlazorPoll.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Plk.Blazor.DragDrop;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BlazorPoll.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddScoped<IPollService, PollService>();
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddBlazoredLocalStorage(config =>
            config.JsonSerializerOptions.WriteIndented = true);
            builder.Services.AddBlazorDragDrop();
            await builder.Build().RunAsync();
        }
    }
}
