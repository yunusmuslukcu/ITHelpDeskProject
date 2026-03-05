using Business.Abstract;
using Business.Concrete;
using Business.Mapping;
using DataAccess.Abstract;
using DataAccess.Context;
using DataAccess.EntityFramework;
using DataAccess.Repositories;
using Entity.Concrete;
using Microsoft.AspNetCore.Identity;

var builder = Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder(args);

// 1. Veritabanı Bağlantısı
builder.Services.AddDbContext<AppDbContext>();

// 2. Identity Yapılandırması
builder.Services.AddIdentity<AppUser, AppRole>(options =>
{
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
})
.AddRoles<AppRole>() // Rol desteğini aktif ettik
.AddEntityFrameworkStores<AppDbContext>();

// 3. Dependency Injection (DI) Kayıtları
builder.Services.AddScoped(typeof(IGenericDal<>), typeof(GenericRepository<>));
builder.Services.AddAutoMapper(typeof(GeneralMapping));
builder.Services.AddScoped<IAccountService, AccountManager>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
});

builder.Services.AddScoped<ICategoryService, CategoryManager>();
builder.Services.AddScoped<ICategoryDal, EFCategoryRepository>();
builder.Services.AddScoped<ITicketService, TicketManager>();
builder.Services.AddScoped<ITicketDal, EFTicketRepository>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// --- SEED DATA OPERASYONU BAŞLANGIÇ ---
// Uygulama her çalıştığında rolleri kontrol eder, yoksa ekler
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();
    string[] roleNames = { "Admin", "Member" };

    foreach (var roleName in roleNames)
    {
        if (!roleManager.RoleExistsAsync(roleName).Result) // Rol yoksa oluştur
        {
            roleManager.CreateAsync(new AppRole { Name = roleName }).Wait();
        }
    }
}
// --- SEED DATA OPERASYONU BİTİŞ ---

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();