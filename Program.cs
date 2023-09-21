using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ResolverQuestao.Context;
using ResolverQuestao.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

string mySqlConnectionStr = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql (mySqlConnectionStr, ServerVersion. AutoDetect (mySqlConnectionStr)));

builder.Services.AddScoped<ISeedUserInitial, SeedUserInitial>();

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.ConfigureApplicationCookie(options => options.LoginPath = "/Usuarios/Login");

builder.Services.ConfigureApplicationCookie(options => options.AccessDeniedPath = "/Usuarios/AcessoNegado");

builder.Services.AddMemoryCache();

builder.Services.AddSession();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequirePadrao", policy => policy.RequireRole("Admin", "SuperUser", "User"));
    options.AddPolicy("RequireSuperUser", policy => policy.RequireRole( "SuperUser", "Admin"));
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

await CriarPerfisUsuariosAsync(app);

app.UseSession();


app.UseAuthentication();
app.UseAuthorization();





app.MapControllerRoute(
    name: "AdminArea",
    pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}");



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



app.Run();



async Task CriarPerfisUsuariosAsync (WebApplication app)
{
   var scopedFactory = app.Services.GetRequiredService<IServiceScopeFactory>();

    using (var scope = scopedFactory.CreateScope())
    {
         var seedUserInitial = scope.ServiceProvider.GetService<ISeedUserInitial>();
    
         await seedUserInitial.SeedRolesAsync();
    
         await seedUserInitial.SeedRolesAsync();
    
         await seedUserInitial.SeedUsersAsync();
    }


}