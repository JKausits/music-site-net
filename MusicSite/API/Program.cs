using MediatR;
using MusicSite.API;
using MusicSite.API.Common;
using MusicSite.API.Features.Shows.Validations;
using MusicSite.API.Middleware;
using MusicSite.API.Persistence;
using MusicSite.API.Settings;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddApplicationDbContext(builder.Configuration);
builder.Services.AddTransient<IPaginator, Paginator>();
builder.Services.AddTransient<IJwtTokenGenerator, JwtTokenGenerator>();
builder.Services.Configure<JwtSettings>(options => builder.Configuration.GetSection("JWTSettings").Bind(options));
builder.Services.AddShowValidation();

builder.Services.AddJwtAuthentication(builder.Configuration);

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(opt =>
{
    opt.AllowAnyMethod().AllowAnyHeader().WithOrigins(builder.Configuration["ClientUrl"]);
});

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
