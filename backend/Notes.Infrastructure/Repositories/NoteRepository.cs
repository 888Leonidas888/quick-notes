using Microsoft.EntityFrameworkCore;
using Notes.Domain.Entities;
using Notes.Domain.Interfaces;
using Notes.Infrastructure.Data;

namespace Notes.Infrastructure.Repositories;

public class NoteRepository(NoteDbContext db) : INoteRepository
{
    public Task<List<Note>> GetAllAsync() =>
        db.Notes.OrderByDescending(n => n.FechaCreacion).ToListAsync();

    public async Task<Note?> GetByIdAsync(int id) =>
        await db.Notes.FindAsync(id);

    public async Task<Note> AddAsync(Note note)
    {
        db.Notes.Add(note);
        await db.SaveChangesAsync();
        return note;
    }

    public async Task DeleteAsync(Note note)
    {
        db.Notes.Remove(note);
        await db.SaveChangesAsync();
    }
}
