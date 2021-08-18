using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.Blazor;

namespace SpaceXAirlines.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);            
            builder.Services.AddSyncfusionBlazor();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NDg3Mjc0QDMxMzkyZTMyMmUzMFhoQS9PYjBUOXNxOC8yakZmL0ZHQ1dyUUVGZFRNY3RDczJIM2cyRUljUnc9;NDg3Mjc1QDMxMzkyZTMyMmUzMGwzMWlKcnJVemlyVSt3bGg4SXpYUXpWSXRPOVJvd3diZWdwMm54Wi93SFE9;NDg3Mjc2QDMxMzkyZTMyMmUzMEY3U01oT0hUcHBJV1grY1lXTkQ4Njd0b1lCeHFuRzJlMWRINUZPRHRSdk09;NDg3Mjc3QDMxMzkyZTMyMmUzMGE0bUZmU1paOHhkWGxoM2NDSVV6UTc1ZHVkTGlBRXpmTnlqU2tVeUpqa1E9;NDg3Mjc4QDMxMzkyZTMyMmUzME5NM3Z5SDFZZDVadHNic1dNL2MzU2thQlZBRVd3aGwxWk5GY0ttam1wT1k9;NDg3Mjc5QDMxMzkyZTMyMmUzMEV0VkM1cVAzSjdTeVJpLzFxT2svd3VaR3haUWErNXFWcUU3b3M2Qmw1M0U9;NDg3MjgwQDMxMzkyZTMyMmUzMGljZUhLRzJhWDY3K1RJd21xZ3JWbkRTelM4NHIwYUN0bXpWRjA3T2Fpd1E9;NDg3MjgxQDMxMzkyZTMyMmUzMFFlN2xLYVR1dzNITmQ1aHI4VHkwTDFia3lLZkV4amZlUHNvTCtDMklhZlk9;NDg3MjgyQDMxMzkyZTMyMmUzMGhSTllhS1ZnakJDTkFpTjRmWUhQejlCWFVjeTRyM2pnM3MyRmVqV3hTVU09;NDg3MjgzQDMxMzkyZTMyMmUzMEs0VjR0WGozUElTZkdLQlpSRiswd2FpaFY0NjNGQ3h1b09aZFVxSWp6TVU9");
            builder.RootComponents.Add<App>("#app");
           
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            await builder.Build().RunAsync();
        }
    }
}
