# MEMORY.md - Fuente de Verdad del Proyecto (quick-notes)

## 1. Plan Aprobado
- **Aplicación:** `quick-notes` (Gestión minimalista de Notas Rápidas para validar el flujo completo de las 4 fases: Local -> PR Filtro TI -> QA en Cloud Run -> Pase a Producción con aprobación de 1 clic).
- **Entidad `Note`:** `Id`, `Titulo`, `Contenido`, `FechaCreacion`.
- **Arquitectura:** Clean Architecture en 4 capas (`Notes.Domain`, `Notes.Application`, `Notes.Infrastructure`, `Notes.Api`) + `Notes.Tests` (`ArchitectureTests` + `NotesApiTests`) y Frontend en `React + TypeScript` sobre **Node 26 LTS**.

## 2. Estado de Tareas
- [x] **Tarea 1:** Aprovisionar repositorio `888Leonidas888/quick-notes` con `init-vicoder-project`.
- [x] **Tarea 2:** Implementar Backend (.NET 10 Clean Architecture) y Frontend (React TS) en rama `feat/initial-quick-notes` y validar tests locales.
- [x] **Tarea 3:** Crear Pull Request, superar Filtro de TI, fusionar a `main` y desplegar en QA esperando aprobación de 1 clic para Producción.
