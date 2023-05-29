using Microsoft.EntityFrameworkCore;
using Orenes.Mapping;
using Orenes.Services.Interfaces;
using Orenes.Repository;
using Orenes.Services.Implementaciones;
using Orenes.Repository.Interfaces;
using Orenes.Repository.Implementaciones;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Context")));

builder.Services.AddScoped<IVehiculoService, VehiculoService>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IUbicacionService, UbicacionService>();
builder.Services.AddScoped<ISecurityService, SecurityService>();
builder.Services.AddScoped<IPedidoService, PedidoService>();
builder.Services.AddScoped<IVehiculoRepository, VehiculoRepository>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IUbicacionRepository, UbicacionRepository>();
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
