# Proposal: Ajustar lógica de ucRegistroCertificadoCalidadCarburantes

## Intent

El control tiene 3281 líneas de code-behind con validación monolítica, lógica duplicada en 3 sitios, ~500 líneas de código muerto, manejo silencioso de errores y formateo decimal frágil dependiente de cultura. Refactorizaremos puntos críticos para reducir deuda técnica y prevenir bugs — sin cambiar comportamiento observable de negocio.

## Scope

### In Scope
1. **Formateo decimal** — Extraer a helper `FormatHelper.ToDecimal(string)` el parsing culture-aware (reemplaza 3+ repeticiones inline)
2. **Validación Cetano 133/134** — Extraer validación cruzada a método `ValidarIndiceCetano()`
3. **Carga de combos en grilla** — Centralizar binding repetido en 3 lugares a método `BindearCombosGrilla(int index)`
4. **Parseo temperatura °C/°F** — Extraer a `ObtenerEspecificacionPorUnidad()`
5. **Código muerto** — Eliminar ~500 líneas comentadas (incluye `grdCertificadoCalidad_HtmlRowCreated` completo)
6. **Catch blocks silenciosos** — Agregar logging a catch blocks vacíos (no cambiar flujo, solo visibilidad)

### Out of Scope
- No cambia arquitectura general (sigue siendo WebForms code-behind)
- No se toca Session
- No se modifican las 3 páginas host
- No se alteran flags de validación dinámica
- No se crean tests unitarios (infraestructura no disponible)

## Capabilities

> Refactor puro — no cambian requisitos a nivel de spec.

- **New Capabilities**: None
- **Modified Capabilities**: None

## Approach

Refactor progresivo (Approach 1 de exploration). Cada extracción es un commit independiente y reversible:

1. Crear `FormatHelper.ToDecimal()` + reemplazar usos inline
2. Extraer `ValidarIndiceCetano()` y `ObtenerEspecificacionPorUnidad()`
3. Centralizar `BindearCombosGrilla()`
4. Eliminar código muerto comentado
5. Agregar logging a catch blocks vacíos

## Affected Areas

| Area | Impact | Description |
|------|--------|-------------|
| `ucRegistroCertificadoCalidadCarburantes.ascx.cs` | Modified | Extracción de métodos (~200 líneas cambian) |
| `CCalidadLibreria.cs` | Modified | Posible destino de helpers |
| `FormatHelper.cs` (nuevo) | New | Helper de formateo decimal |

## Risks

| Risk | Likelihood | Mitigation |
|------|------------|------------|
| Cambio de cultura decimal rompe cálculo con configuración regional distinta | Medium | Mantener detección actual de separador, solo encapsular |
| Catch blocks vacíos son intencionales (errores esperados del negocio) | Medium | **ASK-ON-RISK**: validar con equipo antes de cambiar |
| Código comentado tenía propósito no documentado | Low | Verificar con git blame |
| `btnCancelarRegistroCalidad_Click` vacío: bug o deprecated? | Low | **ASK-ON-RISK**: decidir si implementar o eliminar |

## Rollback Plan

Cada commit tiene su revert independiente. Ningún cambio toca más de un área funcional a la vez. Si falla en QA, se revierte solo ese commit.

## Dependencies

- Ninguna externa. Requiere acceso al servicio REST de validación para pruebas de regresión manuales.

## Success Criteria

- [ ] Proyecto compila sin errores
- [ ] Las 3 páginas host renderizan y funcionan idéntico a antes
- [ ] `grep` confirma que no quedan referencias al código eliminado
- [ ] Catch blocks tienen al menos una llamada a logging
