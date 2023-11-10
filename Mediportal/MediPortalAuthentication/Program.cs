using MediPortalAuthentication.Data;
using MediPortalAuthentication.Extensions;
using MediPortalAuthentication.Model;
using MediPortalAuthentication.Services;
using MediPortalAuthentication.Services.IServices;
using MediPortalAuthentication.Utility;
using MessageBus;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"));
});
builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJwtService,JwtService>();
builder.Services.AddScoped<IRabbitMQPublisherInterface, RabbitMQPublisher>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));
builder.Services.AddCors(options => options.AddPolicy("policy", build =>
{
    build.AllowAnyOrigin();
    build.AllowAnyMethod();
    build.AllowAnyHeader();
}));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("policy");
app.UseAuthorization();
app.UseMigration();
app.MapControllers();

app.Run();
