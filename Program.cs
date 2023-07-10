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
using SpellSmarty.Application.Common.Behaviour;
using FluentValidation;
using SpellSmarty.Application.Common.Validation;
using SpellSmarty.Application.Common.Dtos;
using SpellSmarty.Application.Common.Mappings;

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
//mysql database configuration
var serverVersion = new MySqlServerVersion(new Version(8, 0, 32));

builder.Services.AddDbContext<SpellSmartyContext>(
    dbContextOptions => dbContextOptions
        .UseMySql(builder.Configuration["SpellSmarty2"], serverVersion, options => options.EnableRetryOnFailure(maxRetryCount: 5,
                     maxRetryDelay: System.TimeSpan.FromSeconds(30),
                     errorNumbersToAdd: null))
        //.UseSnakeCaseNamingConvention()
        // The following three options help with debugging, but should
        // be changed or removed for production.
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors()
);



builder.Services.AddSingleton<ITokenServices>(new TokenService(_key, _issuer, _audience, _expirtyMinutes));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddDbContext<SpellSmartyContext>(options => options.UseSqlServer(
//    builder.Configuration.GetConnectionString("SpellSmarty")
//));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IBaseRepository<>),typeof(BaseRepository<>));
builder.Services.AddScoped<IRequestHandler<GetVideosQuery, IEnumerable<VideoDto>>, GetVideosHandler>();
builder.Services.AddScoped<IRequestHandler<GetFeedBackQuery, IEnumerable<FeedBackDto>>, GetFeedBackHandler>();
builder.Services.AddScoped<IRequestHandler<GetVideosByGenreQuery, IEnumerable<VideoDto>>, GetVideosByGenreHandler>();
builder.Services.AddScoped<IRequestHandler<GetVideosByUserIdQuery, IEnumerable<VideoDto>>, GetVideosByUserIdHandler>();
builder.Services.AddScoped<IRequestHandler<GetSingleVideoQuery, VideoDto>, GetSingleVideoHandler>();
builder.Services.AddScoped<IRequestHandler<AuthCommand, AuthResponseDto>, AuthHandler>();
builder.Services.AddScoped<IRequestHandler<GetVideosByCreatorQuery, IEnumerable<VideoDto>>, GetVideosByCreatorHandler>();
builder.Services.AddScoped<IRequestHandler<SaveProgressQuery, string>, SaveProgressHandler>();
builder.Services.AddScoped<IRequestHandler<AddGenreCommand, GenreDto>, AddGenreHandler>();
builder.Services.AddScoped<IRequestHandler<AddVideoCommand, VideoDto>, AddVideoHandler>();
builder.Services.AddScoped<IRequestHandler<SignUpCommand, AccountModel>, SignUpHandler>();
builder.Services.AddScoped<IRequestHandler<VerifyAccountCommand, Task>, VerifyAccountHandler>();
builder.Services.AddScoped<IRequestHandler<UpgradePremiumCommand, AccountModel?>, UpgradePremiumHandler>();
builder.Services.AddScoped<IRequestHandler<UpdateVideoCommand, VideoModel>, UpdateVideoHandler>();
builder.Services.AddScoped<IRequestHandler<CreateVideoCommand, VideoModel>, CreateVideoHandler>();
builder.Services.AddScoped<IRequestHandler<GetAllUserQuery, IEnumerable<AccountModel>>, GetAllUserHandler>();
builder.Services.AddScoped<IRequestHandler<GetUserDetailsQuery, AccountModel?>, GetUserDetailsHandler>();
builder.Services.AddScoped<IRequestHandler<AddVideoGenreCommand, VideoGenreModel>, AddVideoGenreHandler>();
builder.Services.AddScoped<IRequestHandler<UpdateVideoGenreCommand, VideoGenreModel>, UpdateVideoGenreHandler>();
builder.Services.AddScoped<IRequestHandler<LogoutCommand, bool>, LogoutHandler>();


builder.Services.Configure<MailSettings>(builder.Configuration.GetSection(nameof(MailSettings)));
builder.Services.AddTransient<IMailService, MailService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICookieService, CookieService>();
// Register MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

// Add validator
builder.Services.AddScoped<IValidator<AuthCommand>, AuthCommandValidator>();
builder.Services.AddScoped<IValidator<SaveProgressQuery>, SaveProgressCommandValidator>();
builder.Services.AddScoped<IValidator<SignUpCommand>, SignUpCommandValidator>();

// Register behaviour for pipeline
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(StoreCookieBehaviour<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

// Configure AutoMapper
var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.AddProfile<VideoMappingProfile>();
    cfg.AddProfile<VideoStatMappingProfile>();
    cfg.AddProfile<GenreMappingProfile>();
    cfg.AddProfile<FeedBackMappingProfile>();
    cfg.AddProfile<VideoMapper>();
    cfg.AddProfile<AccountMapper>();
    cfg.AddProfile<VideoStatMapper>();
    cfg.AddProfile<GenreMapper>();
    cfg.AddProfile<FeedBackMapper>();
    cfg.AddProfile<VideoGenreMapper>();
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
