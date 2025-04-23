using AttendanceSystem.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Autofac;
using Autofac.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// 🔸 Set AUTOFAC as the DI container
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// 🔸 Configure Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog(); // 🔹 Hook Serilog into the app

// Add services to the container.
builder.Services.AddControllersWithViews();
// 🔸 Configure Entity Framework Core (EF Core) for MySQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("AttendanceDB")!));

// 🔸 Register your custom services or repositories in AUTOFAC
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    // Example: Register a repository and a service
    //containerBuilder.RegisterType<YourRepository>().As<IYourRepository>().InstancePerLifetimeScope();
    //containerBuilder.RegisterType<YourService>().As<IYourService>().InstancePerLifetimeScope();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

try
{
    Log.Information("Starting web application");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application start-up failed");
}
finally
{
    Log.CloseAndFlush();
}
