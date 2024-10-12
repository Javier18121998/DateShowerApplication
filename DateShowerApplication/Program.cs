var builder = WebApplication.CreateBuilder(args);

// Agregar servicios necesarios
builder.Services.AddRazorPages(); // Asegúrate de agregar este servicio

var app = builder.Build();

// Sirve archivos estáticos desde wwwroot
app.UseStaticFiles();

// Habilitar la navegación a Razor Pages
app.MapRazorPages(); // Habilita la navegación a las páginas Razor

// Controlador que devuelve la hora actual
app.MapGet("/time", () =>
{
    // Obtener la hora actual en CDMX, Guadalajara y MTY (UTC-6)
    var currentTime = DateTime.UtcNow.AddHours(-6);

    // Devolver la hora en el formato especificado (incluyendo segundos)
    return Results.Text(currentTime.ToString("yyyy-MM-dd HH:mm:ss"), "text/plain");
});

// Establecer la página de inicio para que solo sirva Index.cshtml
app.MapGet("/", async context =>
{
    context.Response.Redirect("/Index"); // Redirigir a Index.cshtml
});

// Ejecutar la aplicación
app.Run();
