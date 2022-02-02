using System;
using Microsoft.Extensions.DependencyInjection;
using StarWars.Intelligence.Application.Services;

namespace StarWars.Intelligence.Application.ServiceCollectionExtensions
{
    public static class StarWarsIntelligenceCollectionExtensions
    {
        public static void AddIntelligence(this IServiceCollection services, string swapiBaseUrl)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddHttpClient("swapiClient", client =>
            {
                client.BaseAddress = new Uri(swapiBaseUrl);
            });
            services.AddTransient<IIntelligenceService, ApiIntelligenceService>();
        }
    }
}
