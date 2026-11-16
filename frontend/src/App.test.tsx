import { render, screen } from '@testing-library/react';
import { describe, it, expect, vi, beforeEach } from 'vitest';
import App from './App';

describe('Notas Rápidas App', () => {
  beforeEach(() => {
    vi.stubGlobal(
      'fetch',
      vi.fn(() =>
        Promise.resolve({
          ok: true,
          json: () => Promise.resolve([]),
        })
      )
    );
  });

  it('renderiza el formulario y título de Notas Rápidas', async () => {
    render(<App />);
    expect(await screen.findByText('Notas Rápidas')).toBeInTheDocument();
    expect(await screen.findByText('No hay notas registradas.')).toBeInTheDocument();
    expect(screen.getByPlaceholderText('Título de la nota')).toBeInTheDocument();
    expect(screen.getByPlaceholderText('Contenido')).toBeInTheDocument();
    expect(screen.getByRole('button', { name: /Guardar Nota/i })).toBeInTheDocument();
  });
});
