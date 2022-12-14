using JwtNet.Business.Abstract;
using JwtNet.DataAccess.Abstract;
using JwtNet.DataAccess.Concrete.EFCore;
using JwtNet.WebAPI.Business;
using JwtNet.WebAPI.Business.CurrentUser;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
// Connection Database for migration
//builder.Services.AddDbContextPool<ApplicationContext>(options => options
//                        .UseSqlServer("ConnectionStrings:MsSqlConnectionString"));




// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthentication();
//builder.Services.AddSwaggerGen();

// this scopped for services and repository  get stand up when app just have run 
builder.Services.AddScoped<IUserRepository, EFCoreUserRepository>();
builder.Services.AddScoped<IRoleRepository, EFCoreRoleRepository>();
builder.Services.AddScoped<IRefreshTokenRepository, EFCoreRefreshTokenRepository>();

builder.Services.AddScoped<IUserService, UserManager>();
builder.Services.AddScoped<IRoleService, RoleManager>();
builder.Services.AddScoped<IRefreshTokenService, RefreshTokenManager>();

// User.FindFirstValue(ClaimTypes.Name)
builder.Services.AddTransient<ICurrentUser, CurrentUser>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
//builder.Services.AddCors(options => options.AddPolicy(name: "NgOrigins",
//policy =>
//{
//    policy.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader();
//}));
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app cors
app.UseCors("corsapp");
app.UseAuthentication();
app.UseAuthorization();
app.UseSwagger();

app.MapControllers();

app.Run();
