using Microsoft.EntityFrameworkCore;
using MiniCore.Models;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor MVC
builder.Services.AddControllersWithViews();

// Configurar cadena de conexión a SQL Server
builder.Services.AddDbContext<MinicoreContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configurar sesiones
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Tiempo de expiración de sesión
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configuración del pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // Seguridad HTTPS
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Activar sesiones antes de autorización
app.UseSession();

app.UseAuthorization();

// Configurar rutas por defecto
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
