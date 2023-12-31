using cotr.backend.Data;
using cotr.backend.Model;
using cotr.backend.Repository.Exercise;
using cotr.backend.Repository.Language;
using cotr.backend.Repository.User;
using cotr.backend.Service.Command;
using cotr.backend.Service.Email;
using cotr.backend.Service.Encrypt;
using cotr.backend.Service.Exercise;
using cotr.backend.Service.Header;
using cotr.backend.Service.Language;
using cotr.backend.Service.Token;
using cotr.backend.Service.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CotrContext>(
    options => options.UseSqlServer
    (
        builder.Configuration.GetConnectionString("Cotr"), 
        builder => builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null)
    )
);

// Inyección de dependencias de servicios
builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ISecutiryService, SecurityService>();
builder.Services.AddTransient<IHeaderService, HeaderService>();
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddTransient<IExerciseService, ExerciseService>();
builder.Services.AddTransient<ICommandService, CommandService>();
builder.Services.AddTransient<ILanguageService, LanguageService>();

// Inyección de dependencias de repositorios
builder.Services.AddScoped<IUserRepostory, UserRepository>();
builder.Services.AddScoped<IExerciseRepository, ExerciseRepository>();
builder.Services.AddScoped<ILanguageRepository, LanguageReposiotry>();

//Política de CORS
builder.Services.AddCors(opt => opt.AddPolicy("AllowWebapp", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//JWT que elementos del Token se van a comprobar
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer("Access", o =>
    {
        var conf = builder.Configuration.GetSection("JwtConfiguration:TokenApi");
        Jwt jwt = new(
            conf.GetValue<string>("AccessKey") ?? throw new ApiException(500, "No se ha podido cargar la configuración del Token"),
            conf.GetValue<string>("RefreshKey") ?? throw new ApiException(500, "No se ha podido cargar la configuración del Token"),
            conf.GetValue<string>("Issuer") ?? throw new ApiException(500, "No se ha podido cargar la configuración del Token"),
            conf.GetValue<string>("Audience") ?? throw new ApiException(500, "No se ha podido cargar la configuración del Token"),
            conf.GetValue<double>("DurationInMinutesAccess"),
            conf.GetValue<double>("DurationInDaysRefresh")
        );
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
    var conf = builder.Configuration.GetSection("JwtConfiguration:TokenApi");
    Jwt jwt = new(
        conf.GetValue<string>("AccessKey") ?? throw new ApiException(500, "No se ha podido cargar la configuración del Token"),
        conf.GetValue<string>("RefreshKey") ?? throw new ApiException(500, "No se ha podido cargar la configuración del Token"),
        conf.GetValue<string>("Issuer") ?? throw new ApiException(500, "No se ha podido cargar la configuración del Token"),
        conf.GetValue<string>("Audience") ?? throw new ApiException(500, "No se ha podido cargar la configuración del Token"),
        conf.GetValue<double>("DurationInMinutesAccess"),
        conf.GetValue<double>("DurationInDaysRefresh")
    );

    //Jwt jwt = builder.Configuration.GetSection("JwtConfiguration:TokenApi").Get<Jwt>() ?? throw new ApiException(500, "No se ha podido cargar la configuración del token JWT");
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

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "cotr.backend", Version = "v1" });

    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Enter JWT Bearer token **_only_**",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        }
    };
    c.AddSecurityDefinition("Bearer", securityScheme);

    var securityRequirement = new OpenApiSecurityRequirement
    {
        {
            securityScheme,
            new[] { "Bearer" }
        }
    };

    c.AddSecurityRequirement(securityRequirement);
});

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseCors("AllowWebapp");

app.Run();
