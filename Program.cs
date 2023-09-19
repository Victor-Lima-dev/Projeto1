using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ResolverQuestao.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

string mySqlConnectionStr = builder.Configuration.GetConnectionString("DefaultConnection2");

builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql (mySqlConnectionStr, ServerVersion. AutoDetect (mySqlConnectionStr)));

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.ConfigureApplicationCookie(options => options.LoginPath = "/Usuarios/Login");

builder.Services.AddMemoryCache();

builder.Services.AddSession();



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

app.UseSession();


app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();



