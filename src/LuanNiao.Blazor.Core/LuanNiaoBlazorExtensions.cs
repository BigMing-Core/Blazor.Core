using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace LuanNiao.Blazor.Core
{
    public static class LuanNiaoBlazorExtensions
    {
        public static IServiceCollection AddLuanNiaoBlazor(this IServiceCollection services)
        {
            services.AddScoped<WindowEventHub>();
            services.AddScoped<WindowInfo>();
            services.AddScoped<ElementInfo>();
            return services;
        }

    }
}
