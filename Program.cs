using MantaroInclusivo.Application.Services;
using MantaroInclusivo.Domain.Interfaces;
using MantaroInclusivo.Infrastructure.Database;
using MantaroInclusivo.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar conexión a base de datos
builder.Services.AddSingleton<DatabaseConnection>();

// Registro de Repositorios (Infrastructure)
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IDestinoRepository, DestinoRepository>();

// Registro de Servicios (Application)
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<DestinoService>();

// CORS para permitir peticiones del frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

// Servir archivos estáticos
app.UseDefaultFiles();
app.UseStaticFiles();

app.Run();