using Microsoft.EntityFrameworkCore;
using ERP_BACKEND.data;
using ERP_BACKEND.interfaces;
using ERP_BACKEND.services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ERP_BACKEND.helper;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


builder.Services.AddScoped<IAsset, AssetService>();
builder.Services.AddScoped<IUserInterface,UserService>();
builder.Services.AddScoped<IJWTinterface, JWTservice>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// 2. Register AppDbContext with the DI Container
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options=>
{
   options.RequireHttpsMetadata = false;
   options.SaveToken =  true;
   options.TokenValidationParameters =  new TokenValidationParameters
   {
       ValidIssuer = builder.Configuration["JwtConfiguration:Issuer"],
       ValidAudience = builder.Configuration["JwtConfiguration:Audience"],
       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtConfiguration:Key"]!)),
       ValidateIssuer =  true,
       ValidateAudience = true,
       ValidateLifetime = true,
       ValidateIssuerSigningKey = true
       
       
       
   };

   options.Events = new JwtBearerEvents
{
    OnAuthenticationFailed = context =>
    {
        // This will print the exact reason to your console
        Console.WriteLine("Token failed validation: " + context.Exception.Message);
        return Task.CompletedTask;
    }
};

});
builder.Services.AddAuthorization();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthentication(); // 1. Identify user (Checks the JWT)
app.UseAuthorization();  // 2. Check permissions (Now knows who you are)

app.MapControllers();

app.Run();
