using PatCareManagement.Infrastucture.Persistance.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PetCareDbContext>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();


app.Run();
