using Loan_Management_System_Business.Interfaces;

using Loan_Management_System_Business.Services;
using Loan_Management_System_Data.Data;
using Loan_Management_System_Data.Models;
using Loan_Management_System_Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using System.Text;

var builder = WebApplication.CreateBuilder(args);

// =====================================================
// 1. DATABASE CONNECTION
// =====================================================

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("Jwt_Pro2.0")
    ));


// =====================================================
// 2. ASP.NET CORE IDENTITY
// =====================================================

builder.Services
    .AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();


// =====================================================
// 3. JWT AUTHENTICATION
// =====================================================

var jwtKey = builder.Configuration["JwtSettings:Key"];
var jwtIssuer = builder.Configuration["JwtSettings:Issuer"];
var jwtAudience = builder.Configuration["JwtSettings:Audience"];

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme =
            JwtBearerDefaults.AuthenticationScheme;

        options.DefaultChallengeScheme =
            JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,

            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtKey!))
        };
    });


// =====================================================
// 4. REPOSITORIES
// =====================================================

builder.Services.AddScoped<IAuthRepository, AuthRepository>();
// Employee Repository
builder.Services.AddScoped<IEmployeeRepository,EmployeeRepository>();

// Customer Repository
builder.Services.AddScoped<
    ICustomerRepository,
    CustomerRepository>();


// =====================================================
// 5. BUSINESS SERVICES
// =====================================================

builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IJwtService, JwtService>();
// Employee Service
builder.Services.AddScoped<
    IEmployeeService,
    EmployeeService>();

// Customer Service
builder.Services.AddScoped<
    ICustomerService,
    CustomerService>();


// =====================================================
// 6. CONTROLLERS
// =====================================================

builder.Services.AddControllers();


// =====================================================
// 7. SWAGGER
// =====================================================

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Loan Management System API",
        Version = "v1",
        Description = "Loan Management System Backend API"
    });

    // JWT Authentication in Swagger
    options.AddSecurityDefinition(
        "Bearer",
        new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description =
                "Enter JWT token like: Bearer {your-token}"
        });

    options.AddSecurityRequirement(
        new OpenApiSecurityRequirement
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


// =====================================================
// BUILD APPLICATION
// =====================================================

var app = builder.Build();


// =====================================================
// HTTP REQUEST PIPELINE
// =====================================================

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Authentication MUST come before Authorization
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();


// =====================================================
// CREATE DEFAULT ROLES
// =====================================================

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider
        .GetRequiredService<RoleManager<IdentityRole>>();

    string[] roles =
    {
        "Admin",
        "Customer",
        "LoanOfficer",
        "CollectionOfficer"
    };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(
                new IdentityRole(role));
        }
    }
}


app.Run();