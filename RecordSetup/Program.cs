using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using Microsoft.EntityFrameworkCore;
using RecordSetup.Context;
using RecordSetup.Implementation.Repositories;
using RecordSetup.Implementation.Servicies;
using RecordSetup.Interface.Repositories;
using RecordSetup.Interface.Servicies;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContextConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Configuration.AddJsonFile($"appsettings.Dev.json", optional: true);
builder.Configuration.AddEnvironmentVariables();
builder.Services.AddNotyf(config => { config.DurationInSeconds = 10; config.IsDismissable = true; config.Position = NotyfPosition.TopRight; });
//========= DbContext starts =============
builder.Services.AddScoped<IRegionRepository, RegionRepository>();
builder.Services.AddScoped<ISubRegionRecordRepository, SubRegionRecordRepository>();
builder.Services.AddScoped<ISubRegionRecordTableRepository, SubRegionRecordTableRepository>();
builder.Services.AddScoped<ITableSchemaRepository, TableSchemaRepository>();
builder.Services.AddScoped<IRegionService, RegionService>();
builder.Services.AddScoped<ISubRegionRecordService, SubRegionRecordService>();
builder.Services.AddScoped<ISubRegionRecordTableService, SubRegionRecordtableService>();
builder.Services.AddScoped<ISelectListUtilityService, SelectListUtilityService>();
builder.Services.AddScoped<ITableSchemaService, TableSchemaService>();
// Add services to the container.
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

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
app.UseNotyf();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();


app.Run();
