using System.Security.Claims;
using System.Text;
using BookstoreApplication.Controllers.Middleware;
using BookstoreApplication.Data;
using BookstoreApplication.Models;
using BookstoreApplication.Models.Interfaces;
using BookstoreApplication.Repositories;
using BookstoreApplication.Services;
using BookstoreApplication.Services.Interfaces;
using BookstoreApplication.Services.Mappers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Building Example API", Version = "v1" });

    // Definisanje JWT Bearer autentifikacije
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insert JWT token"
    });

    // Primena Bearer autentifikacije
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy
                .WithOrigins("http://localhost:5174")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registracija Identity-a
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// Definisanje uslova koje lozinka mora da ispuni
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;          // Ima bar jednu cifru
    options.Password.RequireLowercase = true;      // Ima bar jedno malo slovo
    options.Password.RequireUppercase = true;      // Ima bar jedno veliko slovo
    options.Password.RequireNonAlphanumeric = true;// Ima bar jedan specijalan karakter (!, @, #...)
    options.Password.RequiredLength = 8;           // Ima bar 8 karaktera
});

// Dodavanje autentifikacije
builder.Services.AddAuthentication(options =>
    { // Naglašavamo da koristimo JWT
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateLifetime = true, 

            ValidateIssuer = true,   
            ValidIssuer = builder.Configuration["Jwt:Issuer"], 

            ValidateAudience = true, 
            ValidAudience = builder.Configuration["Jwt:Audience"], 

            ValidateIssuerSigningKey = true, 
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])), 

            RoleClaimType = ClaimTypes.Role
        };
    });
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IPublisherRepository, PublisherRepository>();
builder.Services.AddScoped<IAwardRepository, AwardRepository>();

builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IPublisherService, PublisherService>();

builder.Services.AddTransient<ExceptionHandlingMiddleware>();

builder.Services.AddAutoMapper(cfg => {
    cfg.AddProfile<MappingProfile>();
});

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowFrontend");
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthorization();
app.MapControllers();

app.Run();
