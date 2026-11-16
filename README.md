# Quick Notes - Aplicación Fullstack (Golden Path)

Aplicación de gestión de notas rápidas construida a partir del template corporativo `app_template_fullstack`.

## Arquitectura
- **Backend (`backend/`):** .NET 10 Minimal API en Clean Architecture (`Notes.Domain`, `Notes.Application`, `Notes.Infrastructure`, `Notes.Api`, `Notes.Tests`).
- **Frontend (`frontend/`):** React + TypeScript + Vite + Vitest sobre Node 26 LTS.
- **Despliegue:** GitHub Actions hacia Google Cloud Run (`quick-notes-api-qa` / `quick-notes-web-qa` en QA y `quick-notes-api-prod` / `quick-notes-web-prod` en Producción).
