using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Configura para aceptar conexiones desde cualquier IP en la red local (192.168.0.3)
builder.WebHost.ConfigureKestrel(options =>
{
    options.Listen(IPAddress.Parse("192.168.0.3"), 7280);  // Esto permite conexiones solo en la IP local y puerto 7280
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Habilitar Swagger solo en el entorno de desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Genera la documentación de Swagger
    app.UseSwaggerUI(); // Muestra la interfaz de Swagger
}

// Elimina o comenta esta línea para deshabilitar la redirección a HTTPS
// app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();
