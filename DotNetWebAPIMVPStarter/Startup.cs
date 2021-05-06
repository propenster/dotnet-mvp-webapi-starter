using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetWebAPIMVPStarter.DAL;
using DotNetWebAPIMVPStarter.Utils;
using DotNetWebAPIMVPStarter.Utils.Config;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Converters;

namespace DotNetWebAPIMVPStarter
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<JwtConfigSettings>(Configuration.GetSection("JwtConfig"));
            services.Configure<SendGridConfig>(Configuration.GetSection("SendGridConfig"));
            services.ConfigureDbContext(Configuration);
            services.ConfigureOAuth(Configuration);
            services.ConfigureDependencies();
            services.ConfigureSwaggerAndJwtAuth(Configuration);
            //services.AddAuthentication(sharedOptions => {
            //    // Registers the cookie auth as the default scheme
            //    sharedOptions.SignInScheme =
            //     CookieAuthenticationDefaults.AuthenticationScheme;
            //});
            //services.AddAuthentication(options => options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme);
            //services.AddMvc();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            services.AddControllersWithViews().AddNewtonsoftJson(options => 
            {
                options.SerializerSettings.Converters.Add(new StringEnumConverter());
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;//still not necessary
            });
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            // Configures cookie auth pipeline
            //app.UseCookieAuthentication(
            // new CookieAuthenticationOptions
            // {
            //     AutomaticAuthenticate = true,
            //     ExpireTimeSpan = TimeSpan.FromDays(4),
            //     SlidingExpiration = false
            // });\    
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                var Prefix = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";
                c.SwaggerEndpoint($"{Prefix}/swagger/v1/swagger.json", "Name of our API");
            });


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
