using System.IO;
using Microsoft.Extensions.FileProviders;


//This is something that Microsoft does to help intelisence.
//Now this extension method will show up as part of the Builder instead of my local folder.
namespace Microsoft.AspNetCore.Builder
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseNodeModules(this IApplicationBuilder app, string root)
        {

            var path = Path.Combine(root, "node_modules");

            var fileProvider = new PhysicalFileProvider(path);

            var options = new StaticFileOptions
            {
                RequestPath = "/node_modules",
                FileProvider = fileProvider
            };

            app.UseStaticFiles(options);

            return app;
        }
    }
}
