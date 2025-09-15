using Db;
using Microsoft.EntityFrameworkCore;
using Repositorys;
using Services;
using UoW;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("TemplatesDb");
builder.Services.AddDbContext<TemplatesDb>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ITemplatesReposytory, TemplatesReposytory>();
builder.Services.AddScoped<TemplatesService>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();

