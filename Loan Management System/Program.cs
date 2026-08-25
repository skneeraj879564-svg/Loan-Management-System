using Loan_Management_System_Business.Dtos.EmailSetting;
using Loan_Management_System_Business.Interfaces;
using Loan_Management_System_Business.Services;
using Loan_Management_System_Data.Data;
using Loan_Management_System_Data.Models;
using Loan_Management_System_Data.Repositories.Implementations;
using Loan_Management_System_Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
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
// 2. EMAIL SETTINGS
// =====================================================

builder.Services.AddSingleton(
    builder.Configuration
        .GetSection("EmailSettings")
        .Get<EmailSettings>()!);


// =====================================================
// 3. ASP.NET CORE IDENTITY
// =====================================================

builder.Services
    .AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();


// =====================================================
// 4. JWT AUTHENTICATION
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
        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = jwtIssuer,
                ValidAudience = jwtAudience,

                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtKey!))
            };
    });


// =====================================================
// 5. REPOSITORIES
// =====================================================

builder.Services.AddScoped<IBranchRepository, BranchRepository>();

builder.Services.AddScoped<IAuthRepository, AuthRepository>();

// Employee Repository
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

builder.Services.AddScoped<ILoanProductRepository, LoanProductRepository>();

builder.Services.AddScoped<ILoanApplicationRepository, LoanApplicationRepository>();

builder.Services.AddScoped<ILoanRepaymentRepository, LoanRepaymentRepository>();

builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();

builder.Services.AddScoped<IPenaltyRepository, PenaltyRepository>();

builder.Services.AddScoped<IForeclosureRepository, ForeclosureRepository>();

builder.Services.AddScoped<IVerificationHistoryRepository,VerificationHistoryRepository>();

builder.Services.AddScoped<ILoanDocumentRepository,LoanDocumentRepository>();

builder.Services.AddScoped< INotificationRepository, NotificationRepository>();

builder.Services.AddScoped< IDashboardRepository,DashboardRepository>();

builder.Services.AddScoped< IReportRepository,ReportRepository>();

// Customer Repository
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

builder.Services.AddScoped< ILoanRepository, LoanRepository>();


// =====================================================
// 6. BUSINESS SERVICES
// =====================================================

builder.Services.AddScoped< IAuthService, AuthService>();

builder.Services.AddScoped< IBranchService,BranchService>();

builder.Services.AddScoped<ILoanProductService,LoanProductService>();

builder.Services.AddScoped<ILoanApplicationService,LoanApplicationService>();

builder.Services.AddScoped<ILoanRepaymentService, LoanRepaymentService>();

builder.Services.AddScoped< IPaymentService, PaymentService>();

builder.Services.AddScoped<IPenaltyService,PenaltyService>();

builder.Services.AddScoped<IForeclosureService,ForeclosureService>();

builder.Services.AddScoped<IVerificationHistoryService,VerificationHistoryService>();

builder.Services.AddScoped< IJwtService, JwtService>();

// =====================================================
// EMAIL SERVICE
// =====================================================

builder.Services.AddScoped<IEmailService,EmailService>();

// Employee Service
builder.Services.AddScoped<IEmployeeService,EmployeeService>();

builder.Services.AddScoped<INotificationService,NotificationService>();

builder.Services.AddScoped< IDashboardService,DashboardService>();

builder.Services.AddScoped<IEmiCalculatorService,EmiCalculatorService>();

// Customer Service
builder.Services.AddScoped< ICustomerService, CustomerService>();

// Loan Document Service
builder.Services.AddScoped< ILoanDocumentService,LoanDocumentService>();

// Report Service
builder.Services.AddScoped<IReportService, ReportService>();

// Loan Service
builder.Services.AddScoped< ILoanService, LoanService>();


// =====================================================
// 7. CONTROLLERS
// =====================================================

builder.Services.AddControllers();


// =====================================================
// 8. SWAGGER
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
                    Reference =
                        new OpenApiReference
                        {
                            Type =
                                ReferenceType.SecurityScheme,

                            Id = "Bearer"
                        }
                },

                Array.Empty<string>()
            }
        });
});


// =====================================================
// 9. BUILD APPLICATION
// =====================================================

var app = builder.Build();


// =====================================================
// 10. HTTP REQUEST PIPELINE
// =====================================================

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); //  ye new add huaa hai 

// Authentication MUST come before Authorization
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();


// =====================================================
// 11. CREATE DEFAULT ROLES + DEFAULT ADMIN
// =====================================================

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

    // ==========================================
    // CREATE ROLES
    // ==========================================

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


    // ==========================================
    // CREATE DEFAULT ADMIN USER
    // ==========================================

    var adminEmail = "admin@gmail.com";

    var adminUser =
        await userManager.FindByEmailAsync(
            adminEmail);

    if (adminUser == null)
    {
        adminUser = new ApplicationUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            EmailConfirmed = true
        };

        var createResult =
            await userManager.CreateAsync(
                adminUser,
                "Admin@123");

        if (createResult.Succeeded)
        {
            await userManager.AddToRoleAsync(
                adminUser,
                "Admin");
        }
    }
    else
    {
        // Make sure existing admin has Admin role
        if (!await userManager.IsInRoleAsync(
                adminUser,
                "Admin"))
        {
            await userManager.AddToRoleAsync(
                adminUser,
                "Admin");
        }
    }
}


// =====================================================
// 12. RUN APPLICATION
// =====================================================

app.Run();