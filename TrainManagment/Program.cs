using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TrainManagment.Data;
using TrainManagment.Data.Repositories;
using TrainManagment.Interfaces;
using TrainManagment.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationContext>(opt =>
{
    //Using Sqlite as test DB for convenience
    opt.UseSqlite(builder.Configuration.GetConnectionString("DBConnection"));
});

builder.Services.AddScoped<IItemRepository, ItemRepository>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
app.UseDeveloperExceptionPage();

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<ApplicationContext>();
await Seed.SeedData(context, builder.Configuration["SeedDataUrl"]!);

app.Run();
