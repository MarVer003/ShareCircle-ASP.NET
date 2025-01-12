using ShareCircle.Data;
using Microsoft.EntityFrameworkCore;
using ShareCircle.Models;
using Microsoft.AspNetCore.Identity;
using ShareCircle.Services;
using System.Globalization;

// Set default culture to sl-SI
CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("sl-SI");
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("sl-SI");

var builder = WebApplication.CreateBuilder(args);

// nastavi spremenljivko connectionString za .useSqlServer(connectionString)
var connectionString = builder.Configuration.GetConnectionString("ShareCircleDbContext");

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IBalanceService, BalanceService>();

// nadomesti stari .AddDbContext
builder.Services.AddDbContext<ShareCircleDbContext>(options =>
options.UseSqlServer(connectionString));

// prilagodi RequireConfirmedAccount = false in .AddRoles<IdentityRole>()
builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ShareCircleDbContext>();

builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ShareCircleDbContext>();
        DbInitializer.Initialize(context); // Pokliče vašo metodo za inicializacijo
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Napaka pri inicializaciji podatkovne baze.");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseSwagger();
app.UseSwaggerUI(c =>
{
c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});

app.UseRouting();

app.UseAuthorization();
// dodaj app.MapRazorPages(); (npr. za app.useAuthentication())
app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
