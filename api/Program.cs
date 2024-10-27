using api.Data;
using api.Interfaces;
using api.Services;
using API.Middlewares;
using API.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>( opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));    
});

builder.Services.AddCors();

builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => 
    {
        var tokenKey = builder.Configuration["TokenKey"] ?? throw new ArgumentNullException(nameof(builder.Configuration["TokenKey"]));
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)),
            ValidateIssuer = false, 
            ValidateAudience = false
        };
    });

var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();


// Configure the HTTP request pipeline.
app.UseCors((cors) => (cors)
.AllowAnyHeader()
.AllowAnyMethod()
.WithOrigins(
    "http://localhost:4200",
    "https://localhost:4200"
));

app.UseAuthorization();

app.UseAuthorization();

app.MapControllers();

app.Run();
