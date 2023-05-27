using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using EmpMgmtWebApi6.Endpoints;
using EmpMgmtWebApi6.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetValue<string>("SQLConnectionString");

builder.Services.AddDbContext<MegaMovieDbContext>(option =>
 option.UseSqlServer(connectionString, contextBuilder =>
 {
     contextBuilder.EnableRetryOnFailure(3);
 }));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapEmployeeEndpoints();

app.MapMovieEndpoints();

app.Run();
