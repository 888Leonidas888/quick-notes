using Notes.Application.DTOs;
using Notes.Domain.Entities;
using Notes.Domain.Interfaces;

namespace Notes.Application.Services;

public class NoteService(INoteRepository repository)
{
    public Task<List<Note>> GetAllAsync() => repository.GetAllAsync();

    public async Task<Note> CreateAsync(NoteInput input)
    {
        var note = new Note
        {
            Titulo = input.Titulo,
            Contenido = input.Contenido,
            FechaCreacion = DateTime.UtcNow
        };
        return await repository.AddAsync(note);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var note = await repository.GetByIdAsync(id);
        if (note is null) return false;

        await repository.DeleteAsync(note);
        return true;
    }
}
