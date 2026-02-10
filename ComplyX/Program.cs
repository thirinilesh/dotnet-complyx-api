
﻿using ComplyX_Businesss.Models;
using ComplyX.Shared.Helper;
using ComplyX.BusinessLogic;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using Jose;
using ComplyX.Services;
using ComplyX.Controllers;
using ComplyX_Businesss.Helper;
using ComplyX_Businesss.Services.Interface;
using ComplyX_Businesss.Services.Implementation;
using ComplyX_Businesss.Helper;
using System.Text.Json.Serialization;
using System.Text.Json;
using ComplyX_Businesss.Services;
using ComplyX_Businesss.BusinessLogic;
using ComplyX.Data.DbContexts;
using ComplyX.Shared.Data;
using ComplyX.Repositories.UnitOfWork;
using ComplyX.Repositories.Repositories.Abstractions;
using ComplyX.Repositories.Repositories;
using AppContext = ComplyX_Businesss.Helper.AppContext;
using ComplyX.Data.Entities;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAcaBusinessAutoMapper(ServiceLifetime.Scoped);
// Add DbContext
builder.Services.AddDbContext< AppDbContext >(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddDbContext<AppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);
// Add Identity
builder.Services.AddIdentity<ApplicationUsers, IdentityRole>()
    .AddEntityFrameworkStores< AppDbContext >()
    .AddDefaultTokenProviders();

// Add custom services
builder.Services.AddScoped<IUserService, UserClass>();
builder.Services.AddScoped<IProductOwner, AccountOwnerLogic>();
builder.Services.AddScoped<IEmployeeServices, EmployeeClass>();
builder.Services.AddScoped<IPayrollServices, PayrollClass>();
//builder.Services.AddScoped<ImportServices, ImportClass>();
builder.Services.AddScoped<LicenseServices, LicenseClass>();
builder.Services.AddScoped<EPFOServices, EPFOClass>();
builder.Services.AddScoped<IGSTServices, GSTClass>();
builder.Services.AddScoped<MasterServices, MasterClass>();
builder.Services.AddScoped<ITTDSServices, ITTDSCClass>();
builder.Services.AddScoped<ComplianceMgmtService, ComplianceMgmtCLass>();
builder.Services.AddScoped<DocumentService, DocumentClass>();
builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
builder.Services.AddRepositories();
builder.Services.AddScoped<AccountOwnerLogic>();
builder.Services.AddScoped<Commanfield>();
builder.Services.AddScoped<Nest.Filter>();
builder.Services.AddScoped<DbContext, ComplyX.Data.DbContexts.AppDbContext>();
builder.Services.AddSingleton<JwtTokenService>();

// Configure JWT
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{

    var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

// Add Controllers
builder.Services.AddControllers()
      .AddJsonOptions(options =>
      {
          options.JsonSerializerOptions.Converters.Add(
              new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, allowIntegerValues: false));
      });
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader()
              .WithExposedHeaders("Authorization");
        });
});

// Swagger


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
{
    var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
    var xmlPath = Path.Combine(System.AppContext.BaseDirectory, $"{assemblyName}.xml");
    if (File.Exists(xmlPath))
        o.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);

    o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    o.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new List<string>()
        }
    });
});

var app = builder.Build();

// Development tools
//if (app.Environment.IsDevelopment())
//{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
//}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();
app.UseCors("AllowAll");
// ✅ Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();