using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using user_api.Data;
using user_api.Models;
using user_api.Services;
using user_api.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
builder.Services.AddScoped<TokenService>();

// set injection depedency for the AgeAuthorization
builder.Services.AddSingleton<IAuthorizationHandler, AgeAuthorization>();

// set Bearer token (authentication method)
builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters{
        ValidateIssuerSigningKey=true,
        IssuerSigningKey= new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes("8567DFDAFYUIASDF876SDAGFUYHJ")
        ),
        ValidateAudience=false,
        ValidateIssuer=false,
        ClockSkew=TimeSpan.Zero
    };
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// set authorization options
builder.Services.AddAuthorization(options => {
    options.AddPolicy("MinimalAge", policy => 
        policy.AddRequirements(new MinimalAge(18))
    );
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
