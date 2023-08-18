using DAL.DBConnector;
using DAL.Interfaces;
using DAL.Models;
using Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<IRepository<TaskApp>, TaskAppRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRepository<User>, UserRepository>();
builder.Services.AddScoped<IPipelineService, PipelineService>();
builder.Services.AddScoped<IRepository<Pipeline>, PipelineRepository>();
builder.Services.AddScoped<DBMyContext>();
//example string connect
string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DBMyContext>(options => options.UseNpgsql(connection));
builder.Services.AddDbContext<DBMyContext>(options => options.LogTo(Console.WriteLine, LogLevel.Information));
//example ioptions
builder.Services.Configure<PipeLineServiceConfig>(builder.Configuration.GetSection("Pipeline"));

var app = builder.Build();
 

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
