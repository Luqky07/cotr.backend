using cotr.backend.Data;
using cotr.backend.Model;
using cotr.backend.Service.Token;
using cotr.backend.Service.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CotrContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Cotr")));

// Inyección de dependencias
builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddTransient<IUserService, UserService>();
//

builder.Services.AddCors(opt => opt.AddPolicy("AllowWebapp", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

if (builder.Environment.IsProduction())
{
    //JWT que elementos del Token se van a comprobar
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer("Access", o =>
        {
            Jwt jwt = builder.Configuration.GetSection("Jwt").Get<Jwt>() ?? throw new ApiException(500, "No se ha podido cargar la configuración del token JWT");
            o.TokenValidationParameters = new TokenValidationParameters
           {
                ValidIssuer = jwt.Issuer,
                ValidAudience = jwt.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.AccessKey)),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true
            };
        }
    ).AddJwtBearer("Refresh", o =>
    {
        Jwt jwt = builder.Configuration.GetSection("Jwt").Get<Jwt>() ?? throw new ApiException(500, "No se ha podido cargar la configuración del token JWT");
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = jwt.Issuer,
            ValidAudience = jwt.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.RefreshKey)),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true
        };
    }
    );

    builder.Services.AddAuthorization();
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsProduction())
{
    app.UseAuthentication();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors("AllowWebapp");

app.Run();
