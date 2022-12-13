using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using Web.Service.DataAccess;
using Web.Service.DataAccess.Entity;
using Web.Service.DataAccess.Implement;
using Web.Service.DataAccess.Interface;
using Web.Service.Extensions;
using Web.Service.Controllers;

var builder = WebApplication.CreateBuilder(args);

#region Fields
string connectionString = builder.Configuration.GetConnectionString("dbconn");
var jwtSection = builder.Configuration.GetSection("Jwt");
var mySqlServerVersion = new MySqlServerVersion(new Version(8,0,22));
var migrationsAssembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name;
#endregion

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

#region Swagger
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(options =>
{
    //swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Value: Bearer {token}",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        Scheme = "Bearer"

    });
    //Json Token
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});
#endregion

#region Using Autofac instead of build-in DI
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder => {
    //Add Type to inject
    builder.RegisterType<UserDao>().As<IUserDao>().InstancePerLifetimeScope();
    builder.RegisterType<VideoDao>().As<IVideoDao>().InstancePerLifetimeScope();
});
#endregion

#region Identity
builder.Services.AddIdentity<User, Role>(opt => {
    //Password rule
    opt.Password.RequireDigit = true;
    opt.Password.RequiredLength = 6;
    opt.Password.RequireLowercase = false;
    opt.Password.RequireUppercase = false;
    opt.Password.RequireNonAlphanumeric = false;
}).AddEntityFrameworkStores<IdentityServerUserDbContext>().AddDefaultTokenProviders();

builder.Services.AddIdentityServer().AddDeveloperSigningCredential()
    .AddConfigurationStore(opt => //Client, Resource, etc.
    {
        opt.ConfigureDbContext = context =>
        {
            context.UseMySql(connectionString, mySqlServerVersion, sql =>
            {
                sql.MigrationsAssembly(migrationsAssembly);
            });
        };
    }).AddOperationalStore(opt => //Codes��Tokens��Consents
    {
        opt.ConfigureDbContext = context =>
        {
            context.UseMySql(connectionString, mySqlServerVersion, sql =>
            {
                sql.MigrationsAssembly(migrationsAssembly);
            });
        };
        opt.EnableTokenCleanup = true;
        //default is 1 hour
        opt.TokenCleanupInterval = 3600;
        opt.TokenCleanupBatchSize = 100;
    }).AddAspNetIdentity<User>();
#endregion

#region Authentication  
// This MUST stay below the AddIdentityServer, otherwise [Authorize] will cause 404 not 401
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opt =>
{
    opt.SaveToken = true;
    opt.RequireHttpsMetadata = true;
    opt.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidIssuer = jwtSection.GetSection("Issuer").Value,
        ValidateAudience = true,
        ValidAudience = jwtSection.GetSection("Audience").Value,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection.GetSection("SecretKey").Value))
    };
});
#endregion

#region Database-MySql
builder.Services.AddDbContext<IdentityServerUserDbContext>(opt =>
{
    opt.UseMySql(connectionString, mySqlServerVersion, options => { options.EnableStringComparisonTranslations(true); });
}).AddDbContext<DBContext>(opt =>
{
    opt.UseMySql(connectionString, mySqlServerVersion, options =>
    {
        // EnableStringComparisonTranslations
        options.EnableStringComparisonTranslations(true);
    });
});
#endregion

var app = builder.Build();

//#region ���ÿ������
//var config_origins = builder.Configuration.GetSection("Origins").Value;
//app.UseCors(builder => builder
//   .WithOrigins(config_origins)
//   .AllowCredentials()
//   .AllowAnyMethod()
//   .AllowAnyHeader());
//#endregion

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseIdentityServer();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MigrateDatabaseAsync().GetAwaiter().GetResult();

app.Run();
