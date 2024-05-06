using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using user_api.Data;
using user_api.Models;
using user_api.Services;

var builder = WebApplication.CreateBuilder(args);

// set db connection
builder.Services.AddDbContext<UserDbContext>(
    opts => opts.UseNpgsql(
        builder.Configuration.GetConnectionString("UserConnection")
    )
);

//set identity
builder.Services
    .AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<UserDbContext>()
    .AddDefaultTokenProviders();

// set automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// set our own classes injection
builder.Services.AddScoped<UserService>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

// app.UseAuthorization();

app.MapControllers();

app.Run();
