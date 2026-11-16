import { useEffect, useState, FormEvent } from 'react';

export interface Note {
  id: number;
  titulo: string;
  contenido: string;
  fechaCreacion: string;
}

const API_URL = import.meta.env.VITE_API_URL || 'http://localhost:8080';

export default function App() {
  const [notes, setNotes] = useState<Note[]>([]);
  const [titulo, setTitulo] = useState('');
  const [contenido, setContenido] = useState('');
  const [error, setError] = useState<string | null>(null);

  const loadNotes = async () => {
    try {
      setError(null);
      const res = await fetch(`${API_URL}/api/notes`);
      if (!res.ok) throw new Error(`HTTP ${res.status}`);
      setNotes(await res.json());
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Error al cargar notas');
    }
  };

  useEffect(() => {
    loadNotes();
  }, []);

  const handleSubmit = async (e: FormEvent) => {
    e.preventDefault();
    try {
      setError(null);
      const res = await fetch(`${API_URL}/api/notes`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ titulo, contenido }),
      });
      if (!res.ok) throw new Error(`HTTP ${res.status}`);
      setTitulo('');
      setContenido('');
      await loadNotes();
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Error al guardar nota');
    }
  };

  const handleDelete = async (id: number) => {
    const res = await fetch(`${API_URL}/api/notes/${id}`, { method: 'DELETE' });
    if (res.ok) await loadNotes();
  };

  return (
    <div className="container">
      <h1>Notas Rápidas</h1>
      {error && <p className="error">{error}</p>}
      <form className="card" onSubmit={handleSubmit}>
        <input
          placeholder="Título de la nota"
          value={titulo}
          onChange={(e) => setTitulo(e.target.value)}
          required
        />
        <textarea
          placeholder="Contenido"
          value={contenido}
          onChange={(e) => setContenido(e.target.value)}
          required
        />
        <button type="submit">Guardar Nota</button>
      </form>
      <div className="card">
        {notes.length === 0 ? (
          <p>No hay notas registradas.</p>
        ) : (
          notes.map((n) => (
            <div key={n.id} className="note-item">
              <div>
                <strong>{n.titulo}</strong>
                <p>{n.contenido}</p>
              </div>
              <button type="button" className="danger" onClick={() => handleDelete(n.id)}>
                Eliminar
              </button>
            </div>
          ))
        )}
      </div>
    </div>
  );
}
