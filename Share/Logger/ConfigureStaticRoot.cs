using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Logger
{
    public static class ConfigureStaticRoot
    {
        public static  void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // removed for brevity

            app.UseStaticFiles();    // for the wwwroot folder

            // for the wwwroot/uploads folder
            string uploadsDir = Path.Combine(env.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsDir))
                Directory.CreateDirectory(uploadsDir);

            app.UseStaticFiles(new StaticFileOptions()
            {
                RequestPath = "/images",
                FileProvider = new PhysicalFileProvider(uploadsDir)
            });

            // the rest of the method
        }
    }
}
