global using MTGDeckBuilder.Services;
global using MTGDeckBuilder.DTO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MTGDeckBuilder.Data;

using System.Reflection;
using MtgApiManager.Lib.Service;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

// Database Stuff
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
var mtgDbContext= builder.Configuration.GetConnectionString("MtgDeckDbContext");
builder.Services.AddDbContext<MtgDeckDbContext>(options =>
    options.UseSqlServer(mtgDbContext));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
//Controller
builder.Services.AddControllersWithViews();
//Identity
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
//Email
builder.Services.AddScoped<IEmailService, EmailService>();
//MtgApiManager.Lib
builder.Services.AddScoped<IMtgServiceProvider, MtgServiceProvider>();

builder.Services.AddRazorPages();

builder.Configuration.AddEnvironmentVariables().AddUserSecrets(Assembly.GetExecutingAssembly(), true);

services.AddAuthentication()
   .AddGoogle(options =>
   {
       options.ClientId = configuration["Google:AppClientId"];
       options.ClientSecret = configuration["Google:AppClientSecret"];
   })
   .AddFacebook(options =>
   {
       options.ClientId = configuration["FbApp:FbAppId"];
       options.ClientSecret = configuration["FbApp:FbAppSecret"];
   })
   .AddMicrosoftAccount(microsoftOptions =>
   {
       microsoftOptions.ClientId = configuration["MSFT:ClientId"];
       microsoftOptions.ClientSecret = configuration["MSFT:ClientSecret"];
   })
   /*.AddTwitter(twitterOptions =>
   {
       twitterOptions.ConsumerKey = configuration["Authentication:Twitter:ConsumerAPIKey"];
       twitterOptions.ConsumerSecret = configuration["Authentication:Twitter:ConsumerSecret"];
       twitterOptions.RetrieveUserDetails = true;
   })*/;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
