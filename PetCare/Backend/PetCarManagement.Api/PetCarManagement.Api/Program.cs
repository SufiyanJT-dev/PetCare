using FluentValidation;
using MediatR;
using Hangfire;
using Hangfire.MemoryStorage; 
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

builder.Services.AddValidatorsFromAssembly(Assembly.Load("PetCareManagement.Application"));

// Register MediatR and the validation behavior
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(Assembly.Load("PetCareManagement.Application"));
    cfg.AddOpenBehavior(typeof(PetCareManagement.Application.Behaviors.ValidationBehavior<,>));
});

builder.Services.AddDbContext<PetCareDbContext>();
builder.Services.AddScoped<IEmailSender, SmtpEmailSender>();
builder.Services.AddScoped<IReminderScheduler, HangfireReminderScheduler>();
builder.Services.AddScoped<ReminderEmailService>();

builder.Services.AddScoped<IUserRepository<User>, UserRepo>();
builder.Services.AddScoped<IGenericRepo<Pets>, PetsRepository>();
builder.Services.AddScoped<IPetRepository, PetsRepository>();
builder.Services.AddScoped<IAuth, AuthRepository>();
builder.Services.AddScoped<IFileStorageService, LocalFileStorageService>();

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

builder.Services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));
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
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials());
});
builder.Services.AddHangfire(config =>
{
    // Use memory storage for demo; replace with SQL Server, Redis, etc.
    config.UseMemoryStorage();
});
builder.Services.AddHangfireServer();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseHangfireDashboard("/hangfire");

app.UseCors("AllowLocalSwagger");


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.UseHttpsRedirection();

app.MapControllers();

app.Run();
