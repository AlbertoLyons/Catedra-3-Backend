using Catedra_3_Backend.src.data;
using Catedra_3_Backend.src.interfaces;
using Catedra_3_Backend.src.models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using DotNetEnv;

using Catedra_3_Backend.src.repositories;

using Microsoft.EntityFrameworkCore;
using System.Text;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(opt => opt.UseSqlite("Data Source=Data.db"));
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<ITokenService, TokenService>();
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

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<DataContext>()
.AddDefaultTokenProviders();
// Añade la autenticación con JWT
builder.Services.AddAuthentication(
opt =>
{
    opt.DefaultAuthenticateScheme = 
    opt.DefaultChallengeScheme = 
    opt.DefaultForbidScheme =
    opt.DefaultScheme =
    opt.DefaultSignInScheme = 
    opt.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opt => {
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = Environment.GetEnvironmentVariable("JWT_IUSSER"),
        ValidateAudience = true,
        ValidAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE"),
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_SIGNINKEY") ?? throw new ArgumentNullException("JWT_SIGINKEY"))),
    };
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = scope.ServiceProvider.GetRequiredService<DataContext>();
    // Crea la base de datos si no existe
    await context.Database.MigrateAsync();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    string[] roleNames = { "User" };
    IdentityResult roleResult;
    // Itera sobre los roles
    foreach (var roleName in roleNames)
    {
        var roleExists = await roleManager.RoleExistsAsync(roleName);
        // Si el role no existe, lo crea
        if (!roleExists)
        {
            roleResult = await roleManager.CreateAsync(new IdentityRole { Name = roleName });
        }
    }
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

