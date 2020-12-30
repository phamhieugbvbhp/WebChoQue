using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChoQueVN.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace ChoQueVN
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //Add to use Session
            services.AddSession();
            //Add to use MVC
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //use Session
            app.UseSession();
            //use Middleware
            app.UseMiddleware<CheckLoginMiddleware>();
            //use folder wwwroot
            app.UseStaticFiles();
            //use MVC

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
            }
            );

            app.UseMvc(routes =>
                {
                    routes.MapRoute(
                        name: "Default",
                        template: "{Controller=Home}/{Action=Index}/{id?}"
                    );
                }
            );
        }
    }
}
