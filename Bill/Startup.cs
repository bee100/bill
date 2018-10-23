using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bill.Database;
using Bill.Entities;
using Bill.Model.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;

namespace Bill
{
    public class Startup
    {

      // This method gets called by the runtime. Use this method to add services to the container.
      // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
      public void ConfigureServices(IServiceCollection services)
        {
            //configure database
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer("Data Source=.\\SQLEXPRESS; Initial Catalog = bill; Integrated Security = True;User Id=bill;Password=bill; MultipleActiveResultSets = True"));

            // Configure JwtIssuerOptions
            services.Configure<JwtIssuerOptions>(options =>
            {
              options.Issuer = "Bill";
              options.Audience = "http://localhost:58562/";
              options.SigningCredentials = new SigningCredentials(AppSettings._signingKey, SecurityAlgorithms.HmacSha256);
            });

             services.AddIdentity<User, IdentityRole>(options =>
             {
               options.Password.RequireDigit = false;
               options.Password.RequireLowercase = false;
               options.Password.RequireUppercase = false;
               options.Password.RequireNonAlphanumeric = false;
               options.Password.RequiredLength = 1;
             })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Bill API", Version = "v1"});
            });

            services.AddAuthentication(options => {
              options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
              options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
              options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
              options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
              options.RequireHttpsMetadata = false;
              options.SaveToken = true;
              options.TokenValidationParameters = new TokenValidationParameters
              {
                ValidIssuer = AppSettings.Issuer,
                ValidAudience = AppSettings.Issuer,
                IssuerSigningKey = AppSettings._signingKey,
                NameClaimType = "name"
              };
              options.Audience = AppSettings.Url;
            });
    }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Bill API");
            });

            app.Use(async (context, next) =>
            {
                await next();
                if(context.Response.StatusCode == 404 &&
                !Path.HasExtension(context.Request.Path.Value) &&
                !context.Request.Path.Value.StartsWith("/api/"))
                {
                    context.Request.Path = "/index.html";
                    await next();
                }
            });

            app.UseAuthentication();
            app.UseMvc();
            app.UseDefaultFiles();
            app.UseStaticFiles();
        }
    }
}
