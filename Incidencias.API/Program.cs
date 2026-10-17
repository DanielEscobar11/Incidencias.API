using Incidencias.API.Data;
using Incidencias.API.Data.Seed;
using Incidencias.API.Interfaces;
using Incidencias.API.Models;
using Incidencias.API.Repositorios;
using Incidencias.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Configuramos la cadena conexiòn a la base de datos
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Reglas contraseñas y bloqueo de usuarios
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    //Reglas de contraseñas: 8 caracterees, al menos un caracter especial y una mayúscula
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;

    //Bloquear usuarios después de 5 intentos fallidos
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);


    options.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();


//INyección de dependencias
builder.Services.AddScoped<TokenServices>();
builder.Services.AddScoped<IIncidenciasRepository, IncidenciaRepository>();
builder.Services.AddScoped<INotaIncidenciaRepository, NotaIncidenciaRepository>();


 
builder.Services.AddControllers()
    //Evitando referencia circular en las respuestas JSON
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    })
    ;
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Configuración la autenticación JWT: es decir las características del token que se va a generar o permitir

var jwtSection = builder.Configuration.GetSection("Jwt");
var signingKey = jwtSection.GetValue<string>("Key");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSection.GetValue<string>("Issuer"),
        ValidAudience = jwtSection.GetValue<string>("Audience"),
        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(signingKey)),
        ClockSkew = TimeSpan.Zero //Elimina el tiempo de tolerancia para la expiración del token
    };
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("cliente-basico", p =>
    p.WithOrigins("http://localhost:5173")
    .AllowAnyHeader()
    .AllowAnyMethod());
});

var app = builder.Build();

using (var scope = app.Services.CreateScope()) 
{
    await DataSeeder.SeedAsync(scope.ServiceProvider);
}

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

app.UseHttpsRedirection();

app.UseCors("cliente-basico");
app.UseAuthentication(); // quien es el usuario
app.UseAuthorization(); // que puede hacer el usuario

app.MapControllers();

app.Run();
