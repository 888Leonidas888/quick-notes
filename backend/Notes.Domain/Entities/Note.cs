namespace Notes.Domain.Entities;

public class Note
{
    public int Id { get; set; }
    public required string Titulo { get; set; }
    public required string Contenido { get; set; }
    public DateTime FechaCreacion { get; set; }
}
