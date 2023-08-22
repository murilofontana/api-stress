using api_stress.DTOs.Request;
using api_stress.Extensions;
using api_stress.Options;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infra.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Xml.Linq;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

ConnectionStrings conncetionStringOptions = new();
builder.Configuration.GetSection(nameof(ConnectionStrings))
    .Bind(conncetionStringOptions);

MigrationsConfiguration migrationsConfigurationOptions= new();
builder.Configuration.GetSection(nameof(MigrationsConfiguration))
    .Bind(migrationsConfigurationOptions);

builder.Services.AddDbContext<DataContext>(options =>
{
  options.UseNpgsql(conncetionStringOptions.Database, npgsqlOptions =>
  {
    npgsqlOptions.MaxBatchSize(100); // Set the maximum batch size for queries
    npgsqlOptions.EnableRetryOnFailure(); // Enable automatic retries on failure
  });
});
builder.Services.AddRepositories();

var app = builder.Build();
//Console.WriteLine($"Valor da váriavel apply migrations{migrationsConfigurationOptions.ApplyMigrations}");

if (migrationsConfigurationOptions.ApplyMigrations)
{
  app.MigrateDatabase();
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/pessoas/{id:guid}", async ([FromRoute(Name = "id")] Guid id, [FromServices]IPessoaRepository _pessoaRepository) => 
{
  var pessoa = await _pessoaRepository.Get(id);

  if (pessoa == null) return Results.NotFound();
  
  return Results.Ok(pessoa);
  
});

app.MapGet("/pessoas", async ([FromQuery(Name = "t")] string searchTerm, [FromServices] IPessoaRepository _pessoaRepository) =>
{
  var pessoa = await _pessoaRepository.SearchByTerm(searchTerm);

  return Results.Ok(pessoa);

});

app.MapPost("/pessoas", async ([FromBody]PessoaRequest requestPessoa, [FromServices] IPessoaRepository _pessoaRepository) =>
{
  try
  {
    var pessoa = new Pessoa(requestPessoa.Apelido, requestPessoa.Nome, requestPessoa.Nascimento, requestPessoa.Stack);
    if (!pessoa.IsValid()) return Results.UnprocessableEntity("UnprocessableEntity");
    await _pessoaRepository.Insert(pessoa);

    return Results.Created(new Uri($"/pessoas/{pessoa.Id}", uriKind: UriKind.Relative), pessoa);

  }
  catch (Exception e)
  {
    return Results.BadRequest(e.Message);
  }
});

app.MapGet("/contagem-pessoas", async ([FromServices] IPessoaRepository _pessoaRepository) =>
{
  var pessoasCount = await _pessoaRepository.CountPessoas();

  return Results.Ok(pessoasCount);

});

app.Run();