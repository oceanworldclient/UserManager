using System.Text.Encodings.Web;
using System.Text.Unicode;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using UserManager.Server;
using UserManager.Server.EntityFramework;
using UserManager.Server.Service;
using UserManager.Server.Utils;
using UserManager.Shared.Converter;


Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateBootstrapLogger(); // <-- Change this line!

try
{
    var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
    var configuration = builder.Configuration;
    AesUtils.Set(configuration["key"]);

    builder.Host.UseSerilog((context, services, config) => config
        .ReadFrom.Configuration(configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext()
    );
    if (!builder.Environment.IsDevelopment())
    {
        builder.Host.UseContentRoot(configuration["ContentRoot"]);
    }
    
    builder.Services.AddDbContext<UserDbContext>(option =>
        option.UseMySQL(configuration.GetConnectionString("Manage").Decrypt()));
    ServiceConfig.Instance.AddConnectionString("World", configuration.GetConnectionString("World").Decrypt());
    ServiceConfig.Instance.AddConnectionString("Ocean", configuration.GetConnectionString("Ocean").Decrypt());
    ServiceConfig.Instance.AddConnectionString("Zebra", configuration.GetConnectionString("Zebra").Decrypt());
    builder.Services.ConfigureIdentity();
    builder.Services.ConfigureJwt(configuration);
    builder.Services.AddControllersWithViews().AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
        opt.JsonSerializerOptions.Converters.Add((new DateTimeConverter()));
        opt.JsonSerializerOptions.Converters.Add(new DateTimeNullableConvert());
    });
    builder.Services.AddResponseCompression(options =>
    {
        options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
            new[] { "application/octet-stream" });
    });
    builder.Services.AddRazorPages();
    builder.Services.AddHttpClient<TelegramBotService>(client =>
        client.BaseAddress = new Uri(configuration["TelegramBotApi"].Decrypt()));
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    builder.Services.AddPanelService();
    builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


    var app = builder.Build();

    AppHttpContext.Configure(app.Services.GetService<IHttpContextAccessor>()!);

// Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseWebAssemblyDebugging();
        //app.UseSwagger();
        //app.UseSwaggerUI();
    }
    else
    {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseForwardedHeaders(new ForwardedHeadersOptions
    {
        ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
    });
    app.UseResponseCompression();
    app.UseHttpsRedirection();

    app.UseBlazorFrameworkFiles();
    app.UseStaticFiles();

    app.UseRouting();
    app.UseAuthentication();
    app.UseAuthorization();


    app.MapRazorPages();
    app.MapControllers();
    app.MapFallbackToFile("index.html");

    app.Run();


}
catch (Exception e)
{
    Log.Fatal(e, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
