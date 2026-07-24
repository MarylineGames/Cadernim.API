using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();
builder.Services.AddControllers();

// Configuração do Banco de Dados com o caminho completo
builder.Services.AddDbContext<Cadernim.API.Data.AppDBContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<Cadernim.API.Repositories.IReceitaRepository, Cadernim.API.Repositories.ReceitaRepository>();

builder.Services.AddCors(Options =>
{
    Options.AddPolicy("LiberarReact", policy =>
    {
        policy.WithOrigins("http://localhost:5173") // Porta do vite
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseCors("LiberarReact");
app.UseAuthorization();
app.MapControllers();

app.Run();