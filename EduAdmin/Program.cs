using EduAdmin.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("connection");
builder.Services.AddDbContext<AppDbContext>(opcions =>
opcions.UseSqlServer(connectionString)
);

// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.AllowAnyOrigin() // Especifica el origen permitido
                     .AllowAnyHeader() // Permite cualquier encabezado
                   .AllowAnyMethod(); // Permite cualquier método (GET, POST, etc.)
        });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); 

builder.Services.AddAutoMapper(typeof(Program));


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors("AllowSpecificOrigin");

app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();
