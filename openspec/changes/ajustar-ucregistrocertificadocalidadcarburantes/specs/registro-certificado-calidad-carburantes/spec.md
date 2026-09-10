# Delta for Registro Certificado Calidad Carburantes

Refactor de `ucRegistroCertificadoCalidadCarburantes.ascx.cs`: extracción de métodos duplicados, eliminación de código muerto y agregado de logging en catch blocks silenciosos. Sin cambio de comportamiento funcional.

## ADDED Requirements

### Requirement: Logging en catch blocks vacíos

Los catch `(Exception)` sin cuerpo funcional DEBEN tener logging vía `CCalidadLibreria.ValidacionAdministradorHydro(ex, ...)` SIN alterar el flujo posterior.

| Escenario | GIVEN | WHEN | THEN |
|-----------|-------|------|------|
| Catch registra y continúa | Catch vacío existente | Se ejecuta | Se registra error + flujo posterior idéntico |
| Múltiples catches | N catches vacíos | Cada uno se ejecuta | Cada uno registra su error |

### Requirement: FormatHelper.ToDecimal() culture-aware

El sistema DEBE proveer `FormatHelper.ToDecimal(string)` que parsee decimal detectando separador de cultura actual, reemplazando el patrón repetido `Convert.ToDecimal(s == "," ? texto.Replace(".", ",") : texto.Replace(",", "."))`.

| Escenario | GIVEN | WHEN | THEN |
|-----------|-------|------|------|
| Punto decimal | "1234.56", cultura "." | ToDecimal("1234.56") | 1234.56m |
| Coma decimal | "1234,56", cultura "," | ToDecimal("1234,56") | 1234.56m |
| Inválido | "abc" | ToDecimal("abc") | FormatException |

## MODIFIED Requirements

### Requirement: Validación cruzada Índice Cetano 133/134

El sistema DEBE extraer la validación cruzada entre especificaciones Cetano (método 133 vs 134) a `ValidarIndiceCetano()`. (Previamente: inline duplicado en grilla)

| Escenario | GIVEN | WHEN | THEN |
|-----------|-------|------|------|
| Comportamiento equivalente | Mismos inputs que inline | Se ejecuta método extraído | Resultado de validación idéntico |

### Requirement: Binding de combos en grilla centralizado

El sistema DEBE centralizar el binding repetido de combos en grilla (3 sitios) a `BindearCombosGrilla(int index)`. (Previamente: duplicado manual)

| Escenario | GIVEN | WHEN | THEN |
|-----------|-------|------|------|
| Binding equivalente | Mismo index + datasource | BindearCombosGrilla(index) | Items, TextField, ValueField idénticos |

### Requirement: Parseo temperatura °C/°F

El sistema DEBE extraer la lógica condicional de unidad de temperatura (~6 repeticiones) a `ObtenerEspecificacionPorUnidad()`. (Previamente: if-else inline repetido)

| Escenario | GIVEN | WHEN | THEN |
|-----------|-------|------|------|
| Unidad °C | Unidad "°C", TempC y TempF | ObtenerEspecificacionPorUnidad("°C", ...) | Retorna valores Celsius |
| Unidad °F | Unidad "°F", TempC y TempF | ObtenerEspecificacionPorUnidad("°F", ...) | Retorna valores Fahrenheit |

## REMOVED Requirements

### Requirement: Código muerto comentado (~500 líneas)

El sistema DEBE eliminar TODO el código comentado sin efecto en ejecución. (Razón: deuda técnica; no tiene propósito documentado.)

| Escenario | GIVEN | WHEN | THEN |
|-----------|-------|------|------|
| Compilación limpia | Archivo .cs | Se compila | Sin errores por referencias a código eliminado |

### Requirement: `grdCertificadoCalidad_HtmlRowCreated` comentado

El sistema DEBE eliminar el handler comentado (~200 líneas). (Razón: duplicación muerta; existe handler activo más arriba.)

| Escenario | GIVEN | WHEN | THEN |
|-----------|-------|------|------|
| Handler único | Archivo .cs | Se busca "grdCertificadoCalidad_HtmlRowCreated" | Solo existe la definición activa |
