using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Ling.Common;
using Ling.Dashboard.Session;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.HttpsPolicy;

namespace Ling.Dashboard
{
    public class Startup
    {
        #region CONSTRUCTOR

        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration config)
        {
            Configuration = config;
        }

        #endregion

        #region METHODS

        // THIS METHOD GETS CALLED BY THE RUNTIME. USE THIS METHOD TO ADD SERVICES TO THE CONTAINER.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDistributedMemoryCache();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            services.AddDistributedMemoryCache(); // Adds a default in-memory implementation of IDistributedCache
            services.AddSession();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<UserSession>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddAuthentication("CookieAuthentication")
                .AddCookie("CookieAuthentication", o =>
                 {
                     o.Cookie.Name = "LoginCookie";
                     o.LoginPath = new PathString("/account/login");
                 });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", p =>
                {
                    p.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });
        }

        // THIS METHOD GETS CALLED BY THE RUNTIME. USE THIS METHOD TO CONFIGURE THE HTTP REQUEST PIPELINE.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var foo = Directory.GetCurrentDirectory();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            app.UseAuthentication();
            //app.UseAuthorization();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();
            //ADD ROUTES
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                  name: "default",
                  template: "{controller=Home}/{action=Index}/{id?}");
            });


        }

        #endregion
    }
}
