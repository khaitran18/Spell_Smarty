using AutoMapper;
using MediatR;
using SpellSmarty.Domain.Interfaces;
using SpellSmarty.Infrastructure;
using SpellSmarty.Infrastructure.Repositories;
using SpellSmarty.Infrastructure.Data;
using SpellSmarty.Application.Mappings;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using SpellSmarty.Application.Queries;
using SpellSmarty.Application.Dtos;
using SpellSmarty.Application.QueryHandlers;
using SpellSmarty.Infrastructure.Mapper;
using SpellSmarty.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using SpellSmarty.Application.CommandHandlers;
using SpellSmarty.Application.Commands;
using SpellSmarty.Domain.Models;
using SpellSmarty.Application.Services;
using static SpellSmarty.Infrastructure.Services.MailService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// For authentication
var _key = builder.Configuration["Jwt:Key"];
var _issuer = builder.Configuration["Jwt:Issuer"];
var _audience = builder.Configuration["Jwt:Audience"];
var _expirtyMinutes = builder.Configuration["Jwt:ExpiryMinutes"];

//services cors
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

// Configuration for token
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidAudience = _audience,
        ValidIssuer = _issuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key)),
        ClockSkew = TimeSpan.FromMinutes(Convert.ToDouble(_expirtyMinutes))

    };
});
builder.Services.AddSingleton<ITokenServices>(new TokenService(_key, _issuer, _audience, _expirtyMinutes));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<SpellSmartyContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("SpellSmarty")
));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IBaseRepository<>),typeof(BaseRepository<>));
builder.Services.AddScoped<IRequestHandler<GetVideosQuery, IEnumerable<VideoDto>>, GetVideosHandler>();
builder.Services.AddScoped<IRequestHandler<GetVideosByUserIdQuery, IEnumerable<VideoDto>>, GetVideosByUserIdHandler>();
builder.Services.AddScoped<IRequestHandler<GetSingleVideoQuery, VideoDto>, GetSingleVideoHandler>();
builder.Services.AddScoped<IRequestHandler<AuthCommand, AuthResponseDto>, AuthHandler>();
builder.Services.AddScoped<IRequestHandler<GetVideosByCreatorQuery, IEnumerable<VideoDto>>, GetVideosByCreatorHandler>();
builder.Services.AddScoped<IRequestHandler<SaveProgressQuery, VideoStatDto>, SaveProgressHandler>();
builder.Services.AddScoped<IRequestHandler<SignUpCommand, AccountModel>, SignUpHandler>();
builder.Services.AddScoped<IRequestHandler<VerifyAccountCommand, Task>, VerifyAccountHandler>();
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection(nameof(MailSettings)));
builder.Services.AddTransient<IMailService, MailService>();

// Register MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));


// Configure AutoMapper
var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.AddProfile<VideoMappingProfile>();
    cfg.AddProfile<VideoStatMappingProfile>();
    cfg.AddProfile<VideoMapper>();
    cfg.AddProfile<AccountMapper>();
    cfg.AddProfile<VideoStatMapper>();
});
var mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseCors("corsapp");
app.UseAuthorization();

app.MapControllers();

app.Run();
