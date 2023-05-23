using HHBooks.API.Configuration;
using HHBooks.API.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connString = builder.Configuration.GetConnectionString("HHBookStoreAppDbConnection");
builder.Services.AddDbContext<HhbookStoreContext>(opitons => opitons.UseSqlServer(connString));
builder.Services.AddIdentityCore<ApiUser>()
                 .AddRoles<IdentityRole>()
                 .AddEntityFrameworkStores<HhbookStoreContext>();


builder.Services.AddAutoMapper(typeof(MapperConfig));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// logging 
builder.Host.UseSerilog((ctx, lc) =>
  lc.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration));

builder.Services.AddCors(opitons =>
{
    opitons.AddPolicy("AllowAll", b => b.AllowAnyMethod()
    .AllowAnyHeader()
    .AllowAnyOrigin());
});
builder.Services.AddAuthentication(opts =>
{
    opts.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
    opts.DefaultChallengeScheme =JwtBearerDefaults.AuthenticationScheme;
})
   .AddJwtBearer(opts =>
   {
       opts.TokenValidationParameters = new TokenValidationParameters
       {
           ValidateIssuerSigningKey = true,
           ValidateIssuer = true,
           ValidateAudience = true,
           ValidateLifetime = true,
           ClockSkew = TimeSpan.Zero,
           ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
           ValidAudience = builder.Configuration["JwtSettings:Audience"],
           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:key"]!))
       };
   });
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
