using AutoMapper;
using LoginAuthUser.Domain.Entities;
using LoginAuthUser.Domain.Interfaces.Repositories;
using LoginAuthUser.Domain.Interfaces.Services;
using LoginAuthUser.Service.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;
using ComandaPro.Domain.Interfaces.Services;
using ComandaPro.CrossCutting.Notifications;
using ComandaPro.Service.Services;
using LoginAuthUser.API.Mapper;
using LoginAuthUser.API.Filters;
using LoginAuthUser.Infra.Data.Context;
using LoginAuthUser.Infra.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

#region [DB]
services.AddDbContext<ApplicationDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("local");
    options.UseSqlServer(connectionString);
});
#endregion

#region [Identity]
services
    .AddIdentity<User, Role>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 8;
});
#endregion

#region [Authentication]
services
    .AddAuthentication(options =>
    {
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
            ValidAudience = builder.Configuration["JWT:ValidAudience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
        };
    });
#endregion

#region [Mapper]            
IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
services.AddSingleton(mapper);
#endregion

#region [DI]
services.AddScoped<NotificationContext>();
services.AddScoped<IBaseService, BaseService>();
services.AddScoped<IAuthService, AuthService>();
services.AddScoped<IUserService, UserService>();
services.AddScoped<IUserRepository, UserRepository>();
#endregion

#region [Swagger]            
services.AddSwaggerGen();
services.AddCors();

services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "LoginAuthUser v1", Version = "v1" });
    c.EnableAnnotations();
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Auth.
                                    Ex: Bearer {token}",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
});
#endregion

services.AddControllers(options =>
{
    options.Filters.Add<NotificationFilter>();
}).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var app = builder.Build();

#region [Migrations and Seeds]
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider
        .GetRequiredService<ApplicationDbContext>();

    dbContext.Database.Migrate();
    dbContext.EnsureSeedData(scope.ServiceProvider);
}
#endregion

#region [Swagger App]            
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "LoginAuthUser v1");
    });
}
#endregion

#region [Cors]            
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());
#endregion

app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();