
using control_clientes_backend.Data;
using control_clientes_backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization; 
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Configurar CORS
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
        options.JsonSerializerOptions.PropertyNamingPolicy = null; // Keep original property names
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; // Ignore reference loops
        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull; // Optionally ignore nulls
    });

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("AllowAngularApp");

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    // Asegúrate de que la base de datos esté creada
    context.Database.EnsureCreated();

    // Verifica si hay clientes en la base de datos
    if (!context.Clientes.Any())
    {
        // Crea datos de ejemplo
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

        // Agrega los clientes y sus direcciones a la base de datos
        context.Clientes.AddRange(clientes);

        // Guarda los cambios
        context.SaveChanges();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

