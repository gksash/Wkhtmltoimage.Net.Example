using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Wkhtmltoimage.Net.Implementation;
using Wkhtmltoimage.Net.Implementation.Interfaces;

namespace Wkhtmltoimage.Net
{
    public static class WkhtmltoimageConfiguration
    {
        public static string WkhtmltoimagePath { get; set; }

        /// <summary>
        /// Setup Rotativa library
        /// </summary>
        /// <param name="services">The IServiceCollection object</param>
        /// <param name="wkhtmltoimageRelativePath">Optional. Relative path to the directory containing wkhtmltopdf. Default is "Rotativa". Download at https://wkhtmltopdf.org/downloads.html</param>
        public static IServiceCollection AddWkhtmltoimage(this IServiceCollection services, string wkhtmltoimageRelativePath = "Executables")
        {
            WkhtmltoimagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, wkhtmltoimageRelativePath);

            if (!Directory.Exists(WkhtmltoimagePath))
            {
                throw new Exception("Folder containing wkhtmltopdf not found, searched for " + WkhtmltoimagePath);
            }

            // var fileProvider = new UpdateableFileProvider();
            services.TryAddTransient<ITempDataProvider, SessionStateTempDataProvider>();
            // services.TryAddSingleton(fileProvider);
            // services.TryAddSingleton<IRazorViewEngine, RazorViewEngine>();
            services.TryAddTransient<IGenerateImage, GenerateImage>();

            return services;
        }
    }
}