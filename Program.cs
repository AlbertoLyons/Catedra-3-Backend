using Catedra_3_Backend.src.data;
using Catedra_3_Backend.src.interfaces;
using Catedra_3_Backend.src.repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(opt => opt.UseSqlite("Data Source=Data.db"));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<Seeder>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyHost", policy =>
    {
        policy.AllowAnyOrigin() 
              .AllowAnyHeader()  
              .AllowAnyMethod(); 
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = scope.ServiceProvider.GetRequiredService<DataContext>();
    // Crea la base de datos si no existe
    await context.Database.MigrateAsync();
    await Seeder.Initialize(services);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAnyHost");
app.UseHttpsRedirection();
app.MapControllers();

app.Run();

