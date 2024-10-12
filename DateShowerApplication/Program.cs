var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor (si fuera necesario)
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Sirve archivos estáticos desde wwwroot
app.UseStaticFiles();

// Manejo de las solicitudes
app.MapGet("/", () => Results.Redirect("/index.html")); // Redirigir a index.html

// Controlador que devuelve la hora actual
app.MapGet("/time", () =>
{
    // Obtener la hora actual en CDMX, Guadalajara y MTY (UTC-5)
    var currentTime = DateTime.UtcNow.AddHours(-6);

    // Devolver la hora en el formato especificado (incluyendo segundos)
    return Results.Text(currentTime.ToString("yyyy-MM-dd HH:mm:ss"), "text/plain");
});

app.Run();