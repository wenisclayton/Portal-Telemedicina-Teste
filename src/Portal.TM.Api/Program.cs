global using Microsoft.AspNetCore.Mvc;
global using MediatR;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Portal.TM.Api.Configuration;
using Portal.TM.Business.Interfaces;
using Portal.TM.Business.Notifications;
using Portal.TM.Business.Services;
using Portal.TM.Data.Context;
using Portal.TM.Data.Repository;

[assembly: ApiConventionType(typeof(DefaultApiConventions))]
var bd = WebApplication.CreateBuilder(args);
var configuration = bd.Configuration;

bd.Services.AddControllers();
bd.Services.AddEndpointsApiExplorer();
//bd.Services.AddSwaggerGen();
bd.Services.AddSwaggerConfiguration("Portal Medicina Teste API V1", "v1");
bd.Services.AddAutoMapper(typeof(Program));
bd.Services.AddMediatR(Assembly.GetExecutingAssembly());
//bd.Services.AddEntityFrameworkInMemoryDatabase().AddDbContext<MyDbContext>(o => o.UseInMemoryDatabase("portal-tm"));
bd.Services.AddDbContext<MyDbContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
});
bd.Services.AddIdentityConfiguration(configuration);
RegisterServices(bd.Services);
var key = Encoding.ASCII.GetBytes(Settings.Secret);
bd.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });


void RegisterServices(IServiceCollection services)
{
    services.AddScoped<IDomainNotificationMediatorService, DomainNotificationMediatorService>();
    services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

    services.AddScoped<IProductService, ProductService>();
    services.AddScoped<IUserService, UserService>();

    services.AddScoped<IProductRepository, ProductRepository>();
    services.AddScoped<IUserRepository, UserRepository>();
}

var app = bd.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
