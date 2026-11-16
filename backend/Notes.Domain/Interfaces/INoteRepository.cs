using Notes.Domain.Entities;

namespace Notes.Domain.Interfaces;

public interface INoteRepository
{
    Task<List<Note>> GetAllAsync();
    Task<Note?> GetByIdAsync(int id);
    Task<Note> AddAsync(Note note);
    Task DeleteAsync(Note note);
}
