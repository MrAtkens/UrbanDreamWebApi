using BazarJok.Contracts.Options;
using BazarJok.DataAccess.Domain;
using BazarJok.DataAccess.Providers;
using BazarJok.Services.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using BazarJok.DataAccess.Models.System;
using BazarJok.Services.Business;
using BazarJok.Services.Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BazarJok.Api.Admin
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration, IHostEnvironment environment)
        {
            _configuration = new ConfigurationBuilder()
                     .AddJsonFile("appsettings.json")
                     .AddJsonFile("appsettings.CoreConfigurations.json")
                     .Build();
        }


        public void ConfigureServices(IServiceCollection services)
        {
            // DbContext
            services.AddDbContext<ApplicationContext>(
                options => options.UseSqlServer(
                    _configuration.GetConnectionString("DevConnection")));
            services.AddTransient<ISmsSender, SmscSmsSender>();
            
            // Providers
            services.AddTransient<BrigadeProvider>();
            services.AddTransient<UserCrudProvider>();
            services.AddTransient<AdminProvider>();
            services.AddTransient<ProblemPinProvider>();
            services.AddTransient<TagProvider>();
            services.AddTransient<ImageProvider>();

            // Authentication
            services.Configure<SecretOption>(_configuration.GetSection("Secrets"));

            services.AddTransient<AdminAuthenticationService>();
            
            // configure jwt authentication
            var secrets = _configuration.GetSection("Secrets");

            var key = Encoding.ASCII.GetBytes(secrets.GetValue<string>("JWTSecret"));
            services.AddAuthentication(x =>
                {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                
            }).AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                    };
                });


            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "UrbanDream.Admin",
                    Description = "WebApi",

                });
                c.AddSecurityDefinition("Bearer",
                    new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Description = "����� JWT ������� ����������� ��������� 'Bearer ' � ��������!",
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey
                    });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                        new string[] { }

                    }
                });
            });

            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            services.AddCors(options =>
            {
                options.AddPolicy(name: "Policy",
                    builder =>
                    {
                        builder
                            .WithOrigins("https://urbandream-admin.netlify.app")
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials();
                    });
            });
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json",
                "UrbanDream.Api.Admin v1"));

            app.UseHttpsRedirection();

            app.UseRouting();
            
            app.UseCors("Policy");

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
