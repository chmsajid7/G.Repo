using API.Controllers;
using API.Extensions;
using Domain.Entities;
using Domain.Seedwork;
using Infrastructure;
using Infrastructure.Contexts;
using Infrastructure.Persistence;
using Infrastructure.Repositories.MongoDb;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.AddMongo(new opt);



// Configure the persistence in another layer
//MongoDbPersistence.Configure();

//builder.Services.AddScoped<IMongoDbContext, MongoDbContext>();
//builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
//builder.Services.AddScoped<IProductRepository, ProductRepository>();



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
