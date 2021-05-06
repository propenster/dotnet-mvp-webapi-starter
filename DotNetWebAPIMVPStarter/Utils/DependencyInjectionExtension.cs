using DotNetWebAPIMVPStarter.DAL;
using DotNetWebAPIMVPStarter.Services.Implementations;
using DotNetWebAPIMVPStarter.Services.Interfaces;
using DotNetWebAPIMVPStarter.Utils.Config;
using DotNetWebAPIMVPStarter.Utils.Email;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetWebAPIMVPStarter.Utils
{
    public static class DependencyInjectionExtension
    {


        public static IServiceCollection ConfigureDbContext(this IServiceCollection services, IConfiguration Config)
        {

            string ConnectionString = Config.GetConnectionString("DefaultConnection");
            services.AddDbContext<DataContext>(options => options.UseSqlServer(ConnectionString));

            return services;
        }

        public static IServiceCollection ConfigureDependencies(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IPostService, PostService>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddCors();
            return services;
        }

        public static IServiceCollection ConfigureOAuth(this IServiceCollection services, IConfiguration Configuration)
        {
            services.Configure<OAuthConfig>(Configuration.GetSection("OAuthConfig"));
            services.Configure<StripeConfig>(Configuration.GetSection("StripeConfig"));
            //services.AddAuthentication(sharedOptions => {
            //    // Registers the cookie auth as the default scheme
            //    sharedOptions.SignInScheme =
            //     CookieAuthenticationDefaults.AuthenticationScheme;
            //});
            return services;
        }
        /// <summary>
        /// Configure Swagger - OpenAPI - and JWT Bearer Authentication for our API
        /// </summary>
        /// <param name="services"></param>
        /// <param name="Configuration"></param>
        /// <returns>IServiceCollection service</returns>
        public static IServiceCollection ConfigureSwaggerAndJwtAuth(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Name of our API", Version = "v1" });
                //of course we could move these hardcoded fields into config file and access them through ICOnfiguration
                c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. Register first with your email and password\r\n\rAfterwards login to obtain a token. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                });
                c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                {
                    {
                                new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[]
                            {

                            }
                    }
                });
            }).AddSwaggerGenNewtonsoftSupport();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;


            }).AddJwtBearer(
                x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["JwtConfig:Issuer"],
                        ValidAudience = Configuration["JwtConfig:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["JwtConfig:SigningKey"]))
                    };
                }
                );

            return services;
        }
    }
}
