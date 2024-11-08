using Microsoft.EntityFrameworkCore;
using perfomanceSystemServer.Interface;
using perfomanceSystemServer.Models;
using perfomanceSystemServer.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<AppDbContext>(obj => obj.UseSqlServer("Server=BFL-COMP-7473\\SQLEXPRESS;Database=PerformanceDB;Trusted_Connection=true;TrustServerCertificate=True;"));
builder.Services.AddTransient<Iuser, UserService>();
builder.Services.AddTransient<Ireviewer, ReviewerService>();
builder.Services.AddTransient<Igoal, GoalService>();
builder.Services.AddTransient<IPerformance, PerformanceService>();
builder.Services.AddTransient<IProfile,ProfileService>();
builder.Services.AddTransient<IDepartment, DepartmentService>();
builder.Services.AddTransient<IDesignation, DesignationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(option=>option.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader());
app.UseAuthorization();

app.MapControllers();

app.Run();
