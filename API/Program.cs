//Esse é o primeiro arquivo que vai rodar na api
using API.Extensions;
using API.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();

//Comentei isso aqui pq um dia pode ser útil
// if (app.Environment.IsDevelopment()) 
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

//app.UseAuthorization(); Não ta usando ainda

app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200"));

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
