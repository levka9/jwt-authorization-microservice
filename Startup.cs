using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using JWT.Auth.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Diagnostics;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using JWT.Auth.Entities;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using Newtonsoft.Json;

namespace JwtWebTokenSerice
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
            AddConnectionStrings(ref services);

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                                    //.WithOrigins("http://localhost",
                                    //"https://localhost",
                                    //"http://localhost:4200")
                                      .AllowAnyMethod()
                                      .AllowAnyHeader()
                                    //.AllowCredentials()
                                      );
            });

            services.AddAutoMapper(typeof(Startup));

            services.AddControllers()
                    .AddNewtonsoftJson(o =>
                    {
                        o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    });

            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            
            #region Authentication
            // configure jwt authentication
            //var appSettings = appSettingsSection.Get<AppSettings>();
            //var key = Encoding.UTF8.GetBytes(appSettings.Secret);

            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //        .AddJwtBearer(x =>
            //        {
            //            x.RequireHttpsMetadata = false;
            //            x.SaveToken = true;                        
            //            x.TokenValidationParameters = new TokenValidationParameters
            //            {                            
            //                ValidateIssuerSigningKey = true,
            //                IssuerSigningKey = new SymmetricSecurityKey(key),
            //                ValidateIssuer = true,
            //                ValidateAudience = true,
            //                ValidIssuer = "http://localhost:4000",
            //                ValidAudience = "http://localhost:30778"
            //            };
            //        }); 
            #endregion

            // configure DI for application services            
            //services.AddScoped<IWebTokenIdentityGenericRepository<Token, long>, TokenRepository>();
            //services.AddScoped<IUserRepository, UserRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            // global cors policy

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //loggerFactory.AddFile("Log/log-{Date}.txt", isJson:true);
            
            // TODO replace from app settings            
            app.UseCors("CorsPolicy");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void AddConnectionStrings(ref IServiceCollection services)
        {
            services.AddDbContext<JWTAuthContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("JWTAuthDefaultConnection")));
        }
    }
}
