
using AutoMapper;
using Common.BLL.Services.EntityServices;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using SMS.BLL.AutoMapper;
using SMS.BLL.Models.Configurations;
using SMS.BLL.Services.AuthenticationServices;
using SMS.BLL.Services.AuthenticationServices.Interfaces;
using SMS.BLL.Services.EntityServices;
using SMS.BLL.Services.EntityServices.Interfaces;
using SMS.DAL.Data;
using SMS.DAL.Repositories;
using SMS.DAL.Repositories.interfaces;
using SMSCore.Models.Entities;
using System.Text;

namespace SMSCore.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static WebApplicationBuilder AddDbContext(this WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("Default");

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            return builder;
        }

        public static WebApplicationBuilder AddEntityServices(this WebApplicationBuilder builder)
        {
            builder.Services
                .AddScoped<IRepositoryBase<User>, RepositoryBase<User>>()
                .AddScoped<IRepositoryBase<University>, RepositoryBase<University>>()
                .AddScoped<IRepositoryBase<Email>, RepositoryBase<Email>>()
                .AddScoped<IRepositoryBase<EmailTemplate>, RepositoryBase<EmailTemplate>>()
                .AddScoped<IRepositoryBase<Verification>, RepositoryBase<Verification>>();

            builder.Services
                .AddScoped<IEntityBaseService<User>, EntityBaseService<User, IRepositoryBase<User>>>()
                .AddScoped<IEntityBaseService<University>, EntityBaseService<University, IRepositoryBase<University>>>()
                .AddScoped<IEntityBaseService<Email>, EntityBaseService<Email, IRepositoryBase<Email>>>()
                .AddScoped<IEntityBaseService<EmailTemplate>, EntityBaseService<EmailTemplate, IRepositoryBase<EmailTemplate>>>()
                .AddScoped<IEntityBaseService<Verification>, EntityBaseService<Verification, IRepositoryBase<Verification>>>();

            builder.Services
                .AddScoped<IUserService, UserService>()
                .AddScoped<IUniversityService, UniversityService>()
                .AddScoped<IEmailService, EmailService>()
                .AddScoped<IEmailTemplateService, EmailTemplateService>()
                .AddScoped<IVerificationService, VerificationService>();

            return builder;
        }

        public static WebApplicationBuilder AddBLLServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IAuthenticaitonService, AuthenticationService>();
            builder.Services.AddScoped<IJWTGeneratorService, JWTGeneratorService>();

            return builder;
        }

        public static WebApplicationBuilder AddMappingConfiguration(this WebApplicationBuilder builder)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();

            builder.Services.AddSingleton(mapper);

            return builder;
        }

        public static WebApplicationBuilder AddConfiguraitons(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<AuthenticationConfigurations>(builder.Configuration.GetSection(AuthenticationConfigurations.Position));
            builder.Services.Configure<EmailConfigurations>(builder.Configuration.GetSection(EmailConfigurations.Position));

            return builder;
        }

        public static WebApplicationBuilder AddCustomAuthentication(this WebApplicationBuilder builder)
        {
            var jwtConfiguration = builder.Configuration.GetSection(AuthenticationConfigurations.Position).Get<AuthenticationConfigurations>();

            builder.Services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = "JWT_OR_COOKIE";
                opt.DefaultChallengeScheme = "JWT_OR_COOKIE";

            }).AddCookie(options =>
            {
                options.LoginPath = "/auth/signin";
                options.ExpireTimeSpan = TimeSpan.FromDays(1);

            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = jwtConfiguration.Issuer,
                    ValidAudience = jwtConfiguration.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration.Key)),
                };

            }).AddPolicyScheme("JWT_OR_COOKIE", "JWT_OR_COOKIE", options =>
            {
                options.ForwardDefaultSelector = context =>
                {
                    string authorization = context.Request.Headers[HeaderNames.Authorization];
                    if (!string.IsNullOrEmpty(authorization) && authorization.StartsWith("Bearer "))
                        return JwtBearerDefaults.AuthenticationScheme;

                    return CookieAuthenticationDefaults.AuthenticationScheme;
                };
            });

            return builder;
        }

        public static WebApplicationBuilder AddMVC(this WebApplicationBuilder builder)
        {
            builder.Services.AddMvc(options =>
            {
                options.MaxModelBindingCollectionSize = 50;
                options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(
                     _ => "The field is required.");
            });

            return builder;
        }
    }
}
