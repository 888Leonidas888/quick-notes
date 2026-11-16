using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Notes.Application.DTOs;
using Notes.Domain.Entities;

namespace Notes.Tests;

public class NotesApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public NotesApiTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.WithWebHostBuilder(builder =>
        {
            builder.UseEnvironment("Testing");
        }).CreateClient();
    }

    [Fact]
    public async Task HealthEndpoint_ReturnsOk()
    {
        var response = await _client.GetAsync("/health");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task CreateAndGetNote_WorksCorrectly()
    {
        var newNote = new NoteInput("Nota de prueba", "Validando el flujo Golden Path a Produccion");

        var postResponse = await _client.PostAsJsonAsync("/api/notes", newNote);
        Assert.Equal(HttpStatusCode.Created, postResponse.StatusCode);

        var created = await postResponse.Content.ReadFromJsonAsync<Note>();
        Assert.NotNull(created);
        Assert.True(created.Id > 0);
        Assert.Equal("Nota de prueba", created.Titulo);

        var notes = await _client.GetFromJsonAsync<List<Note>>("/api/notes");
        Assert.NotNull(notes);
        Assert.Contains(notes, n => n.Titulo == "Nota de prueba");
    }
}
