# Exploration: Ajustar lógica del formulario ucRegistroCertificadoCalidadCarburantes

## Current State

`ucRegistroCertificadoCalidadCarburantes` es un UserControl de ASP.NET WebForms (~3281 líneas de code-behind) que maneja el registro, modificación y eliminación de certificados de calidad de carburantes/lubricantes. Está alojado en tres páginas distintas:

- **wfRegistroCertificadoCalidad.aspx** — Registro nuevo (TipoDeRegistro=1), con árbol de entidades
- **wfGestionCertificadoCalidad.aspx** — Modificación/Eliminación (TipoDeRegistro=2/3), desde gestión
- **wfRegistroCertificadoCalidad.aspx** (también usado para modificación indirecta)

### Flujo principal

1. **Carga**: `Page_Load` → `CargarParametroGrillaCalidad()` → `CargaGrilla()`
2. **Validación**: `btnGuardarRegistroCalidad_Click` (~570 líneas de validación)
3. **Confirmación**: Popup `ppMensajeAlerta` → `btnContinuar_OnClick` → `guardarObjCalidad()`
4. **Persistencia**: Servicio REST vía `JsonServiceClient` (POST a `/RegistraPruebasCalidad/` o `/ActualizaPruebasCalidad/`)

### Arquitectura de validación dinámica

El control usa un mecanismo de "validación por configuración": un servicio REST devuelve una lista de `O_VALIDA_CALIDAD_CTY` que contiene flags como `PRECIO`, `VOLUMEN_MUESTRA`, `RESOLUCION`, `MARCA_PRODUCTO`, `NOMBRE_PRODUCTO`, `TIPO_OPERACION`, `ENTIDADES_RELACIONADOS`, `PRODUCTOS_BASE`, `NRO_TK_PRECINTOS`, `FECHA_MUIM`, `FECHA_RA`, `PRUEBA_CERRADA`, `PTC`, `VOLUMEN`, `ADJUNTA_DOCUMENTO_CTR`, `SIN_OBS`, `SIN_PARAMETRO`. Estos flags determinan qué campos se muestran y cómo se validan.

---

## Affected Areas

### Core files

| Archivo | Rol | Líneas |
|---------|-----|--------|
| `Comun/UControl/ucRegistroCertificadoCalidadCarburantes.ascx.cs` | Lógica completa del control | 3281 |
| `Comun/UControl/ucRegistroCertificadoCalidadCarburantes.ascx` | Markup ASPX con grid, paneles, validadores | 563 |
| `Comun/UControl/ucRegistroCertificadoCalidadCarburantes.ascx.designer.cs` | Designer generado automáticamente | 978 |
| `Sitio/VolumenesCalidad/GestionCalidad/wfRegistroCertificadoCalidad.aspx.cs` | Página host para registro nuevo | 299 |
| `Sitio/VolumenesCalidad/GestionCalidad/wfGestionCertificadoCalidad.aspx.cs` | Página host para modificación/eliminación | 115 |
| `Sitio/VolumenesCalidad/GestionCalidad/wfRegistroCertificadoCalidad.aspx` | Markup de la página host | ~50 |
| `Sitio/VolumenesCalidad/GestionCalidad/wfGestionCertificadoCalidad.aspx` | Markup de la página host | ~35 |

### Supporting files

| Archivo | Rol |
|---------|-----|
| `Parametros/VolumenesCalidad/CParametrosCalidad.cs` | Constantes para keys de Session/parámetros |
| `Clases/VolumenesCalidad/CCalidadLibreria.cs` | Utilidades de logging y validación |

### Servicios REST consumidos

| Endpoint | Propósito |
|----------|-----------|
| `GET /ListarPruebasCalidadTablaEspecificacion/` | Carga la grilla de pruebas/ensayos |
| `GET /ObtenerValidacionCalidad/` | Obtiene flags de validación configurables |
| `GET /ReportarCalidad/` | Obtiene datos de un certificado existente (modificación) |
| `POST /RegistraPruebasCalidad/` | Guarda nuevo certificado |
| `POST /ActualizaPruebasCalidad/` | Actualiza certificado existente |
| `POST /EliminaRegistroCalidad/` | Elimina certificado |
| `POST /RegistraAlertaCalidad/` | Registra alerta si corresponde |
| `POST /RegistraDocumento/` | Adjunta documento PDF |
| `POST /GestionVolumen/` | Registro de volumen cero |

---

## Key Findings & Problem Areas

### 1. Validación monolítica y desordenada

El método `btnGuardarRegistroCalidad_Click` (líneas 2073-2643) contiene ~570 líneas con toda la validación inline:
- Validaciones de campos obligatorios (fechas, combos, TAG, unidad)
- Validación cruzada de Índice de Cetano/Número de Cetano (IDs 133/134)
- Parseo de valores con `>` y `<` para rangos
- Conversión de temperatura °C/°F desde especificaciones compuestas
- Verificación de API y NLGI con lógica de bytes ASCII
- Construcción de DataTable para popup de confirmación

**Riesgo**: Difícil de mantener, probar y modificar sin efectos colaterales.

### 2. Duplicación de lógica de grilla

El bloque de carga de combos (MetodoASTM, Unidad, IsoNlgi) dentro de la grilla aparece repetido en **tres lugares**:
- `CargaGrilla()` (original)
- `limpiar()` (copia casi idéntica)
- `grdCertificadoCalidad_CustomCallback()` (tercera copia)

Todos iteran sobre `vColFormularioEspecifico` y bindean los mismos combos.

### 3. Problemas de cultura y formato decimal

El código tiene múltiples TODO comentados sobre el formato de decimales:
```csharp
var s = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;
vObjPruebaCalidad.op_debe = Convert.ToDecimal(s == "," ? TextBoxVolumen.Text.Replace(".", ",") : TextBoxVolumen.Text.Replace(",", "."));
```
Esta lógica se repite para Volumen, Precio y VolumenMuestra. Hay código comentado de intentos anteriores con diferentes enfoques.

### 4. Flujo de Volumen Cero como toggle

El switch `ckbxVolumenCero` alterna entre `panelRegistroNormal` y `panelVolumenCero`. Cuando está activo, se oculta el formulario normal y se muestra solo un campo de fecha con botón "REGISTRAR". El servicio `/GestionVolumen/` maneja este caso especial.

### 5. Uso intensivo de Session

Casi todo el estado se pasa via `Session[...]`:
- Listas de unidades de medida, marcas, productos, puntos de custodia
- Validaciones de calidad
- Valores de grilla
- Documento digital (byte[] del PDF adjunto)

Esto hace que el control sea frágil en escenarios de múltiples pestañas o timeouts de sesión.

### 6. Código muerto y comentado

Grandes bloques de código comentado (~500 líneas en total):
- Método `grdCertificadoCalidad_HtmlRowCreated` completo comentado (duplicado del activo)
- Versiones anteriores de validación de API/NLGI
- Múltiples intentos de formateo de decimales
- `btnCancelarRegistroCalidad_Click` sin implementación

### 7. Manejo de errores silencioso

Varios catch blocks están vacíos o solo re-asignan la alerta:
```csharp
catch (Exception ex) { }
```
Esto oculta errores que podrían ser importantes.

---

## Approaches

### Approach 1: Refactor progresivo con extracción de métodos

**Descripción**: Sin cambiar la arquitectura general, extraer la lógica de validación, carga de grilla y formateo decimal en métodos privados con nombre semántico.

- **Pros**:
  - Bajo riesgo de regresión
  - Mejora la legibilidad inmediatamente
  - Se puede hacer por capas (validación → carga → persistencia)
  - Fácil de revisar en PRs pequeños

- **Cons**:
  - No resuelve la deuda técnica estructural
  - El método `btnGuardarRegistroCalidad_Click` sigue siendo grande

- **Effort**: Medium (~2-3 días)

### Approach 2: Separación de responsabilidades con clases de servicio

**Descripción**: Extraer la lógica de negocio a clases separadas (`CertificadoCalidadValidator`, `GrillaQualityBuilder`, `PruebaCalidadMapper`) y dejar el code-behind solo como coordinador de UI.

- **Pros**:
  - Separación clara de concerns
  - Testeable unitariamente
  - Prepara el terreno para migración a MVC/MVC Core

- **Cons**:
  - Mayor esfuerzo inicial
  - Riesgo de romper la funcionalidad existente
  - Requiere entender el dominio a fondo

- **Effort**: High (~1 semana)

### Approach 3: Enfoque híbrido (extracción selectiva + refactor de hot spots)

**Descripción**: Identificar los puntos más críticos (validación de Cetano 133/134, formateo decimal, duplicación de carga de grilla) y refactorizar solo esos, dejando el resto del control intacto.

- **Pros**:
  - Máximo impacto con mínimo riesgo
  - Resultados visibles rápidamente
  - Se pueden encadenar PRs pequeños

- **Cons**:
  - No resuelve toda la deuda técnica
  - Puede dejar inconsistencias si no se planifica bien

- **Effort**: Low-Med (~1-2 días)

---

## Recommendation

**Approach 1 (Refactor progresivo)** es el camino recomendado. Razones:

1. El control tiene 3281 líneas — cualquier cambio estructural mayor (Approach 2) requiere un entendimiento completo del negocio que actualmente no está documentado
2. Hay 3 páginas host que usan el control — cambios arquitectónicos podrían afectar a todas
3. El mecanismo de validación dinámica (flags desde servicio) hace que el comportamiento sea difícil de predecir sin acceso al entorno real
4. Se pueden hacer PRs pequeños y revisables: primero extraer validación de decimales, luego lógica de Cetano, luego duplicación de grilla

**Componentes a refactorizar primero** (por orden de impacto/riesgo):

1. **Formateo decimal** — Unificar en un helper `FormatHelper.ToDecimal(string)` 
2. **Validación 133/134** — Extraer a `ValidarIndiceCetano(...)`
3. **Carga de combos en grilla** — Extraer a `BindearCombosGrilla(visibleIndex)`
4. **Parseo de temperatura °C/°F** — Extraer a `ObtenerEspecificacionPorUnidad(...)`
5. **Construcción de DataTable para popup** — Extraer a `ConstruirTablaResumen()`

---

## Risks

- **Alto acoplamiento con Session**: No se puede probar unitariamente sin mockear HttpContext
- **Comportamiento condicional por flags**: Sin acceso al servicio de validación, no se puede verificar qué combinaciones de flags existen en producción
- **Cultura de decimales**: Cambiar el formateo podría romper el cálculo en diferentes configuraciones regionales del servidor
- **Tres páginas host**: Cualquier cambio en la interfaz del control (propiedades, eventos) requiere verificar las 3 páginas
- **Sin tests existentes**: No hay proyectos de prueba unitaria para este control (AnhPruebaUnitaria no contiene tests relacionados)

---

## Unknowns & Assumptions

### Unknowns (requieren validación)

1. ¿Qué combinaciones de flags `O_VALIDA_CALIDAD_CTY` existen realmente en producción? Solo vemos `VOLUMEN`, `PRECIO`, `VOLUMEN_MUESTRA`, `RESOLUCION`, `MARCA_PRODUCTO`, `NOMBRE_PRODUCTO`, `TIPO_OPERACION`, `ENTIDADES_RELACIONADOS`, `PRODUCTOS_BASE`, `NRO_TK_PRECINTOS`, `FECHA_MUIM`, `FECHA_RA`, `PRUEBA_CERRADA`, `PTC`, `ADJUNTA_DOCUMENTO_CTR`, `SIN_OBS`, `SIN_PARAMETRO`, `PERMITE_MODIFICAR` — pero puede haber más.
2. ¿El servicio `/GestionVolumen/` es el mismo para todas las actividades o varía?
3. ¿Qué actividades usan el flujo de "Productos Base" (IDs 358, 360, 361, 362, 383)?
4. ¿Cuál es el comportamiento esperado para el `btnCancelarRegistroCalidad_Click` (actualmente vacío)?
5. ¿La lógica de ASCII-byte para validar API (líneas 298-388) está probada en producción o es código heredado que nadie entiende?

### Assumptions

1. El flujo de "volumen cero" es un caso edge poco usado
2. Los flags `FECHA_MUIM` y `FECHA_RA` son mutuamente excluyentes
3. La entidad con ID 11, 12, 19969 no requiere adjuntar archivo PDF
4. Los servicios REST están disponibles y responden con el contrato esperado
5. El proyecto compila con .NET Framework 4.x (por las referencias a DevExpress v15.1, AjaxControlToolkit 4.1)

---

## Ready for Proposal

**Sí**, hay suficiente información para proceder con `sdd-propose`. Sin embargo, recomiendo:

1. **Aclarar el alcance** con el usuario: ¿es un refactor de validación, una corrección de bugs, o una mejora funcional?
2. **Validar los unknowns** con el equipo antes de diseñar la solución
3. **Confirmar** si el `btnCancelarRegistroCalidad_Click` vacío es un bug o está deprecated

El exploration artifact se ha persistido en:
- Engram: `sdd/ajustar-ucregistrocertificadocalidadcarburantes/explore`
- Filesystem: `openspec/changes/ajustar-ucregistrocertificadocalidadcarburantes/exploration.md`
