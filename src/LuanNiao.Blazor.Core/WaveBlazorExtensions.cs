using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace LuanNiao.Blazor.Core
{
    public static class WaveBlazorExtensions
    {
        public static IServiceCollection AddWaveBlazor(this IServiceCollection services)
        { 
            services.AddScoped<WindowEventHub>();
            return services;
        }

    }
}
