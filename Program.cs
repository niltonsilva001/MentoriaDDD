using MentoriaDDD.Data;
using MentoriaDDD.Repositories;
using MentoriaDDD.Repositories.Interfaces;
using MentoriaDDD.Services;
using MentoriaDDD.Validador;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Serviços
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Logging.AddConsole();

// Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Repositories
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();

// Services
builder.Services.AddScoped<IClienteService, ClienteService>();

// Validadores (registrar manualmente)
builder.Services.AddScoped<CriarClienteValidador>();
builder.Services.AddScoped<AtualizarClienteValidador>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
