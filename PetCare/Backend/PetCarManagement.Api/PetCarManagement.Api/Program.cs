using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using PatCareManagement.Infrastucture.Persistance.Data;

using PetCareManagement.Application.IRepository;
using PetCareManagement.Domain.Entity;
using PetCareManagement.Infrastucture.Persistance.Repository;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Register validators for DI (do NOT enable automatic MVC validation)
builder.Services.AddValidatorsFromAssembly(Assembly.Load("PetCareManagement.Application"));

// Register MediatR and the validation behavior
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(Assembly.Load("PetCareManagement.Application"));
    cfg.AddOpenBehavior(typeof(PetCareManagement.Application.Behaviors.ValidationBehavior<,>));
});

// DbContext (configure provider/connection string in real app)
builder.Services.AddDbContext<PetCareDbContext>();

builder.Services.AddScoped<IGenericRepo<User>, UserRepo>();
builder.Services.AddScoped<IAuth, AuthRepository>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

// JWT auth (optional)
var jwtKey = builder.Configuration["Jwt:Key"];
if (!string.IsNullOrEmpty(jwtKey))
{
    var keyBytes = Encoding.UTF8.GetBytes(jwtKey);
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = true;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(keyBytes)
        };
    });
}
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalSwagger", policy =>
        policy.WithOrigins("https://localhost:7121", "https://localhost:5001")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials());
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Exception middleware must be registered early so it can catch exceptions from controllers and MediatR pipeline

app.UseCors("AllowLocalSwagger");


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Standard pipeline ordering
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
