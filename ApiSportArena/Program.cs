using ApiSportArena.Data;
using ApiSportArena.Repository;
using ApiSportArena.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var chave = new SymmetricSecurityKey(RandomNumberGenerator.GetBytes(32));

builder.Services.AddSingleton(chave);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = "ApiSportArena",
        ValidateAudience = true,
        ValidAudience = "SportArena",
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = chave,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        ValidAlgorithms = new[] { SecurityAlgorithms.HmacSha256 }
    };
}
);


builder.Services.AddAuthorization();

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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
