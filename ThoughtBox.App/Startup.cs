using System.Net;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ThoughtBox.App.Data;
using ThoughtBox.App.Repositories;
using ThoughtBox.App.Services;

namespace ThoughtBox.App
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // GDPR stuff
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // Configures Ef Core
            services.AddDbContext<AppDbContext>(
                options => options.UseMySql(Configuration.GetConnectionString("Default"))
            );

            // https://nicolas.guelpa.me/blog/2017/01/11/dotnet-core-data-protection-keys-repository.html
            services.AddSingleton<IXmlRepository, DataProtectionKeyRepository>();
            var built = services.BuildServiceProvider();
            services.AddDataProtection()
                .AddKeyManagementOptions(options => options.XmlRepository = built.GetService<IXmlRepository>());

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = "Twitter";
            })
            .AddCookie()
            .AddTwitter("Twitter", options =>
            {
                options.ConsumerKey = Configuration["Twitter:ApiKey"];
                options.ConsumerSecret = Configuration["Twitter:ApiSecretKey"];
                options.SaveTokens = true;
            });

            services.AddScoped<IThoughtService, ThoughtService>();
            services.AddScoped<IViewService, ViewService>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseHttpsRedirection();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All,
                RequireHeaderSymmetry = false,
                ForwardLimit = null,
                KnownNetworks = { new IPNetwork(IPAddress.Parse("::ffff:10.0.0.5"), 104) }
            });
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseCookiePolicy();
            app.UseMvcWithDefaultRoute();
        }
    }
}
