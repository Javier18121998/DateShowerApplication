var builder = WebApplication.CreateBuilder(args);

// Agregar servicios necesarios
builder.Services.AddRazorPages();

var app = builder.Build();

// Sirve archivos estáticos desde wwwroot
app.UseStaticFiles();

// Habilitar la navegación a Razor Pages
app.MapRazorPages();

// Controlador que devuelve la hora actual
app.MapGet("/time", (string timezone) =>
{
    DateTime currentTime;

    // Ajustar la hora actual según la zona horaria seleccionada
    switch (timezone)
    {
        case "Sydney":
            currentTime = DateTime.UtcNow.AddHours(11); // UTC+11
            break;
        case "Australia":
            currentTime = DateTime.UtcNow.AddHours(10); // UTC+10
            break;
        case "Russia":
            currentTime = DateTime.UtcNow.AddHours(3); // UTC+3
            break;
        default:
            currentTime = DateTime.UtcNow.AddHours(-6); // UTC-6 para CDMX
            break;
    }

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
