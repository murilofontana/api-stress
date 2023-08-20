using api_stress.DTOs.Request;
using api_stress.Extensions;
using api_stress.Options;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infra.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

ConnectionStrings conncetionStringOptions = new();
builder.Configuration.GetSection(nameof(ConnectionStrings))
    .Bind(conncetionStringOptions);


builder.Services.AddDbContext<DataContext>(options => options.UseNpgsql(conncetionStringOptions.Database));
builder.Services.AddRepositories();

var app = builder.Build();

app.MigrateDatabase();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
  var forecast = Enumerable.Range(1, 5).Select(index =>
     new WeatherForecast
     (
         DateTime.Now.AddDays(index),
         Random.Shared.Next(-20, 55),
         summaries[Random.Shared.Next(summaries.Length)]
     ))
      .ToArray();
  return forecast;
})
.WithName("GetWeatherForecast");

app.MapGet("/pessoas/{id:guid}", async ([FromRoute(Name = "id")] Guid ? id, [FromServices]IPessoaRepository _pessoaRepository) => 
{
  var pessoa = await _pessoaRepository.Get(id ?? Guid.NewGuid());
  
  return pessoa;
  
});

app.MapPost("/pessoas", async ([FromBody]PessoaRequest requestPessoa, [FromServices] IPessoaRepository _pessoaRepository) =>
{
  try
  {
    var pessoa = new Pessoa(requestPessoa.Apelido, requestPessoa.Nome, requestPessoa.Nascimento, requestPessoa.Stack);
    await _pessoaRepository.Insert(pessoa);

    return Results.Created(new Uri($"/pessoas/{pessoa.Id}"), pessoa);

  }
  catch (Exception e)
  {
    return Results.BadRequest(e.Message);
  }
});

app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
  public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}