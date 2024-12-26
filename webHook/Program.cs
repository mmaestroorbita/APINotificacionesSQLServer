using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NotificationService.Services;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor
builder.Services.AddControllers(); // Habilitar controladores
builder.Services.AddEndpointsApiExplorer(); // Para Swagger (opcional)
builder.Services.AddSwaggerGen(); // Documentación Swagger (opcional)
builder.Services.AddScoped<IReportService, ReportService>();
var app = builder.Build();

// Configurar el pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // Mostrar detalles de errores en desarrollo
    app.UseSwagger(); // Habilitar Swagger en desarrollo
    app.UseSwaggerUI();
}

app.UseHttpsRedirection(); // Redireccionar a HTTPS
app.UseRouting();
app.UseAuthorization();

app.MapControllers(); // Mapear controladores

app.Run();
