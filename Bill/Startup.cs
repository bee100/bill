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
      private const string SecretKey = "yVn7LajtZQ+c22xige+tJBIvt/ypwvAhlPznDDS6d9lmO9PdjgBYuYo34EGCmrLRSMHOIR8CKI7VLuNLl8sQ14ZOaOKwsRc+yi+D2VbElBWEM+PvMcXGQTJbtiwH5Iw5kHNdhgEBf2knkt7CsMUNMkREP9G+Of0ObU9onC6Fb2Q4G5+7jPhGCyFhFhE0VjKIpjf26BucWMWJu5K8UNO5S3P44kvUFxbU8Yt9T9/mBGEiRVSeW+UcNKOKIgGPlRG4hrVYHt2BPANcr7AJ/ilxOWhfnmyqPrtt2OnT6Y49UUd/9pzLynkSSsCXJ4PRAA37xTSoQ3Jt9+YUII1++nCHgQ==";

      private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

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
              options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
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

            services.AddAuthentication().AddJwtBearer(options =>
            {
              options.TokenValidationParameters = new TokenValidationParameters
              {
                ValidateIssuer = true,
                ValidIssuer = "Bill",

                ValidateAudience = true,
                ValidAudience = "http://localhost:58562/",

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signingKey,

                RequireExpirationTime = false,
                ValidateLifetime = false,
                ClockSkew = TimeSpan.Zero
              };
              options.Audience = "http://localhost:58562/";
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
            app.UseMvcWithDefaultRoute();
            app.UseDefaultFiles();
            app.UseStaticFiles();
        }
    }
}
