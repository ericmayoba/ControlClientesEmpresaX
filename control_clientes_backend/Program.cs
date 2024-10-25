
using control_clientes_backend.Data;
using control_clientes_backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization; 
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", builder =>
    {
        builder.WithOrigins("http://localhost:4200") 
               .AllowAnyMethod() 
               .AllowAnyHeader(); 
    });
});


builder.Services.AddDbContext<AppDbContext>(opt => {
    opt.UseSqlServer(builder.Configuration.GetConnectionString("SQLServerConnection"));
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null; 
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; 
        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull; 
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("AllowAngularApp");

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    context.Database.EnsureCreated();

    if (!context.Clientes.Any())
    {
        var clientes = new List<Cliente>
        {
            new Cliente
            {
                Nombre = "Juan Perez",
                Email = "juan.perez@example.com",
                Telefono = "555-1234",
                OtrosDatos = "Cliente VIP",
                Direcciones = new List<Direccion>
                {
                    new Direccion
                    {
                        Calle = "123 Calle Principal",
                        Sector = "Ciudad A",
                        Provincia = "Provincia A",
                        Pais = "País A"
                    },
                    new Direccion
                    {
                        Calle = "456 Calle Secundaria",
                        Sector = "Ciudad B",
                        Provincia = "Provincia B",
                        Pais = "País B"
                    }
                }
            },
            new Cliente
            {
                Nombre = "Maria Lopez",
                Email = "maria.lopez@example.com",
                Telefono = "555-5678",
                OtrosDatos = "Cliente recurrente",
                Direcciones = new List<Direccion>
                {
                    new Direccion
                    {
                        Calle = "789 Calle Terciaria",
                        Sector = "Ciudad C",
                        Provincia = "Provincia C",
                        Pais = "País C"
                    }
                }
            }
        };
        context.Clientes.AddRange(clientes);
        context.SaveChanges();
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

