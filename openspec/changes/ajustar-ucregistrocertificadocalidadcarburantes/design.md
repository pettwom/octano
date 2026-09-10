# Design: Ajustar lógica de ucRegistroCertificadoCalidadCarburantes

## Technical Approach

Refactor progresivo del code-behind (3281 líneas) extrayendo 4 métodos duplicados, eliminando ~366 líneas de código muerto y agregando logging en 5 catch blocks silenciosos. Sin cambiar comportamiento funcional observable. Cada cambio es un commit independiente y reversible.

## Architecture Decisions

| Decisión | Opciones | Decisión | Rationale |
|----------|----------|----------|-----------|
| ¿Dónde poner FormatHelper? | En `CCalidadLibreria.cs` vs archivo nuevo | **Archivo nuevo `FormatHelper.cs`** | Cohesión: no mezclar helpers de formateo con lógica de validación. Sigue el patrón de la solución (cada clase en su archivo). |
| ¿Mover BindearCombosGrilla a clase aparte? | En el mismo code-behind vs clase helper | **En el mismo code-behind** | Solo el control usa estas combinaciones; no hay razón para exponerlo. |
| ¿Logging como delegado o inline? | Inline vs método helper | **Inline `CCalidadLibreria.ValidacionAdministradorHydro(ex,...)`** | Ya es el patrón existente (líneas 1507-1510, 2639-2640). Consistencia > abstracción. |
| ¿Tocar catch en btnEliminar que ya muestra alerta? | No tocar vs agregar logging | **Agregar logging** | Spec lo exige; `alert.Visible` es UI, no logging. No se altera el mensaje al usuario. |

## Data Flow

```
FormatHelper.ToDecimal(texto)
  ↑ detecta CultureInfo.CurrentCulture
  ↑ reemplaza separador según cultura
  ↑ return decimal

ValidarIndiceCetano(i, vObjValorReportado, ...)
  ↑ evalúa id_prueba_cal == 133/134
  ↑ valida cruzado con row anterior/posterior
  ↑ setea alertas y varRes

ObtenerEspecificacionPorUnidad(valor, unidad)
  ↑ parsea string "100°C/212°F" según unidad °C/°F
  ↑ retorna solo la parte relevante

BindearCombosGrilla(index, colFormulario)
  ↑ bindea vObjMetodoASTM, vObjIsoNlgi, vObjUnidad
  ↑ usado por: HtmlRowCreated, CustomCallback, limpiar()
```

## File Changes

| Archivo | Acción | Descripción |
|---------|--------|-------------|
| `Comun/UControl/ucRegistroCertificadoCalidadCarburantes.ascx.cs` | **Modificar** | Extraer 4 métodos, eliminar código muerto, agregar logging. ~200 líneas cambian neto. |
| `Clases/VolumenesCalidad/FormatHelper.cs` | **Crear** | Helper `ToDecimal(string)` culture-aware. |
| `Clases/VolumenesCalidad/CCalidadLibreria.cs` | Sin cambios | No se modifica. |

## Interfaces / Contracts

```csharp
// FormatHelper.cs (nuevo)
public static class FormatHelper {
    public static decimal ToDecimal(string valor)
}

// En ucRegistroCertificadoCalidadCarburantes.ascx.cs (nuevos métodos)
private void BindearCombosGrilla(int index,
    List<E_TABLA_ESPECIFICA> colFormularioEspecifico)
private int ValidarIndiceCetano(int rowIndex, TextBox vObjValorReportado,
    decimal idPruebaCal, decimal valorAnterior, decimal valorPosterior,
    TextBox vObjValorReportadoAnt, TextBox vObjValorReportadoPost)
private string ObtenerEspecificacionPorUnidad(string valor,
    string unidad, bool esMaxima)
```

## Testing Strategy

| Capa | Qué probar | Cómo |
|------|-----------|------|
| Compilación | Todo el proyecto | `msbuild AnhOctanoV2.sln` — sin errores |
| Regresión manual | Las 3 páginas host | Renderizado idéntico, grilla, guardado, validación Cetano |
| Logging | Catch blocks | Verificar traza en sistema de logs de ADMINISTRADOR HYDRO |
| Código muerto | grep reverso | Confirmar que no hay referencias a lo eliminado |

## Migration / Rollout

No requiere migración de datos. Rollback por commit revert individual.

## Catch Blocks Audit (logging a agregar)

| Línea | Método | Estado actual | Acción |
|-------|--------|---------------|--------|
| 736 | `CargarOpcionAdjuntar()` | catch vacío | Agregar `ValidacionAdministradorHydro(ex, ...)` |
| 2704 | `btnEliminarRegistroCalidad_Click` | solo alert UI | Agregar logging manteniendo alert |
| 2878 | método interno | catch vacío | Agregar `ValidacionAdministradorHydro(ex, ...)` |
| 2913 | `ckbxVolumenCero_CheckedChanged` | catch vacío | Agregar `ValidacionAdministradorHydro(ex, ...)` |
| 2983 | `btnRegistrarVolumenCero_Click` | catch vacío | Agregar `ValidacionAdministradorHydro(ex, ...)` |

**NO TOCAR**: `btnCancelarRegistroCalidad_Click` (línea 2666, handler vacío — respetar constraint).

## Open Questions

Ninguna. Spec y proposal cubren todos los escenarios.
