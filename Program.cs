using Microsoft.EntityFrameworkCore;
using LecturasApi.Data;

var builder = WebApplication.CreateBuilder(args);

// 👉 CLAVE PARA DOCKER/RENDER
builder.WebHost.UseUrls("http://0.0.0.0:80");

// 👉 CORS para permitir conexión desde Blazor u otros clientes
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// 👉 Servicios principales
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

// 👉 Configura base de datos SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// 👉 Crea la base de datos si no existe (clave para evitar errores en Render)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated(); // Puedes cambiar por .Migrate() si usas migraciones reales
}

// 👉 Middleware y enrutamiento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();