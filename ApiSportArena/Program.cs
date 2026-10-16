using ApiSportArena.Data;
using ApiSportArena.Services;
using ApiSportArena.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<UsuarioRepository>();
builder.Services.AddScoped<ProdutoService>();
builder.Services.AddScoped<ProdutoRepository>();
builder.Services.AddScoped<CategoriasService>();
builder.Services.AddScoped<CategoriasRepository>();
builder.Services.AddScoped<CarrinhoService>();
builder.Services.AddScoped<CarrinhoRepository>();
builder.Services.AddScoped<CarrinhoItemService>();
builder.Services.AddScoped<CarrinhoItemRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
