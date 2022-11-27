using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
// using Microsoft.OpenApi.Models;
using UserManager.Server.EntityFramework;
using UserManager.Server.EventHub.EventHandler;
using UserManager.Server.Service;

namespace UserManager.Server;

public static class PanelServiceCollectionExtensions
{
    public static void AddPanelService(this IServiceCollection services)
    {
        services.AddScoped<BoughtService>();
        services.AddScoped<UserService>();
        services.AddScoped<ShopService>();
        services.AddScoped<UserRoleService>();
        services.AddScoped<OperationLogService>();
        services.AddSingleton(new BuyShopLogger());
        services.AddSingleton(new CloseRenewLogger());
        services.AddSingleton(new DeleteBoughtLogger());
        services.AddSingleton(new ModifyPasswordLogger());
        services.AddSingleton(new ModifyUserLogger());
        services.AddSingleton(new UpgradeShopLogger());
        services.AddSingleton(new IllegalOperationLogger());
        services.AddSingleton(new RestoreBoughtLogger());
        services.AddScoped<AuthService>();
    }
    
    public static void ConfigureControllers(this IServiceCollection services)
    {
        services.AddControllers(config =>
        {
            config.CacheProfiles.Add("30SecondsCaching", new CacheProfile
            {
                Duration = 30
            });
        });
    }
    public static void ConfigureResponseCaching(this IServiceCollection services) => services.AddResponseCaching();


    public static void ConfigureIdentity(this IServiceCollection services)
    {
        services.AddIdentity<IdentityUser, IdentityRole>(o =>
            {
                o.Password.RequireDigit = true;
                o.Password.RequireUppercase = false;
                o.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<UserDbContext>()
            .AddDefaultTokenProviders();
    }

    public static void ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtConfig = configuration.GetSection("JwtConfig");
        services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtConfig["ValidIssuer"],
                    ValidAudience = jwtConfig["ValidAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig["Secret"]))
                };
            });
    }
    
    // public static void ConfigureSwagger(this IServiceCollection services)
    // {
    //     services.AddSwaggerGen(c =>
    //     {
    //         c.SwaggerDoc("v1", new OpenApiInfo
    //         {
    //             Title = "客服Api",
    //             Version = "v1",
    //             Description = "客服系统api",
    //             Contact = new OpenApiContact
    //             {
    //                 Name = "客服系统"
    //             },
    //         });
    //         c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
    //         c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    //         {
    //             Name = "Authorization",
    //             Type = SecuritySchemeType.ApiKey,
    //             Scheme = "Bearer",
    //             BearerFormat = "JWT",
    //             In = ParameterLocation.Header,
    //             Description = "JWT Authorization header using the Bearer scheme."
    //         });
    //         
    //         c.AddSecurityRequirement(new OpenApiSecurityRequirement
    //         {
    //             {
    //                 new OpenApiSecurityScheme
    //                 {
    //                     Reference = new OpenApiReference
    //                     {
    //                         Type = ReferenceType.SecurityScheme,
    //                         Id = "Bearer"
    //                     }
    //                 },
    //                 Array.Empty<string>()
    //             }
    //         });
    //     });
    // }
    
}