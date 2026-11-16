using Microsoft.EntityFrameworkCore;
using Notes.Application.DTOs;
using Notes.Application.Services;
using Notes.Infrastructure;
using Notes.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(
    builder.Configuration,
    builder.Environment.IsEnvironment("Testing"));

builder.Services.AddCors(options =>
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

var app = builder.Build();
app.UseCors();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<NoteDbContext>();
    db.Database.EnsureCreated();
    if (db.Database.IsRelational())
    {
        db.Database.ExecuteSqlRaw(@"
            IF OBJECT_ID(N'dbo.Notes', N'U') IS NULL
            CREATE TABLE dbo.Notes (
                Id INT IDENTITY(1,1) PRIMARY KEY,
                Titulo NVARCHAR(200) NOT NULL,
                Contenido NVARCHAR(MAX) NOT NULL,
                FechaCreacion DATETIME2 NOT NULL
            );");
    }
}

app.MapGet("/health", () => Results.Ok(new { status = "healthy" }));

app.MapGet("/api/notes", async (NoteService service) =>
    Results.Ok(await service.GetAllAsync()));

app.MapPost("/api/notes", async (NoteInput input, NoteService service) =>
{
    var created = await service.CreateAsync(input);
    return Results.Created($"/api/notes/{created.Id}", created);
});

app.MapDelete("/api/notes/{id:int}", async (int id, NoteService service) =>
    await service.DeleteAsync(id) ? Results.NoContent() : Results.NotFound());

app.Run();

public partial class Program { }
