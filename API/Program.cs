//Esse é o primeiro arquivo que vai rodar na api

using API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<DataContext>(options => 
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));//Pega do appsettings.json
});

var app = builder.Build();

// Configure the HTTP request pipeline.

//Comentei isso aqui pq um dia pode ser útil
// if (app.Environment.IsDevelopment()) 
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

//app.UseAuthorization(); Não ta usando ainda

app.MapControllers();

app.Run();
