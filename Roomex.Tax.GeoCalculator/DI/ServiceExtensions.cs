using Microsoft.Extensions.DependencyInjection;
using Roomex.Task.Core;

namespace Roomex.Task.GeoCalculator.DI;

public static class ServiceExtensions
{
    /// <summary>
    /// Adds the default implementation of <see cref="IDistanceCalculator"/> into the DI
    /// </summary>
    /// <param name="services"></param>
    public static void AddDistanceCalculator(this IServiceCollection services)
    {
        services.AddTransient<IDistanceCalculator, DistanceCalculator>();
    }
}