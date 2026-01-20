using Microsoft.EntityFrameworkCore;
using ERP_BACKEND.data;
using ERP_BACKEND.interfaces;
using ERP_BACKEND.services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ERP_BACKEND.helper;
using Swashbuckle.AspNetCore.SwaggerUI;
 // This should now resolve correctly


using Microsoft.OpenApi;
using Scalar.AspNetCore; // Note: No .Models here


var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// ... inside builder.Services

builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        document.Info.Title = "ERP API";
        document.Info.Version = "v1";

        const string schemeId = "Bearer";

        // 1. Define the Security Scheme using the concrete class
        var securityScheme = new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "Enter your JWT token"
        };

        // 2. Initialize Components and map to the Interface dictionary
        document.Components ??= new OpenApiComponents();
        document.Components.SecuritySchemes = new Dictionary<string, IOpenApiSecurityScheme>
        {
            [schemeId] = securityScheme
        };

        // 3. Create a Reference (Required in v3.x)
        var schemeReference = new OpenApiSecuritySchemeReference(schemeId, document);

        // 4. Apply globally to the document
        document.Security = new List<OpenApiSecurityRequirement>
        {
            new OpenApiSecurityRequirement
            {
                [schemeReference] = new List<string>()
            }
        };

        return Task.CompletedTask;
    });
});

builder.Services.AddScoped<IAsset, AssetService>();
builder.Services.AddScoped<IUserInterface,UserService>();
builder.Services.AddScoped<IJWTinterface, JWTservice>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IRackService, RackService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ITurbineService, TurbineService>();
builder.Services.AddScoped<IStoreService, StoreService>();

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
    app.MapScalarApiReference();
 // This creates /openapi/v1.json
    
    app.UseSwaggerUI(options =>
    {
        // This is the important part: point to the .NET 10 endpoint
       options.SwaggerEndpoint("/openapi/v1.json", "ERP API v1");

       
        // This ensures the page is hosted at /swagger
        options.RoutePrefix = "swagger";
    });
}

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
    
}

app.UseAuthentication(); // 1. Identify user (Checks the JWT)
app.UseAuthorization();  // 2. Check permissions (Now knows who you are)

app.MapControllers();

app.Run();
