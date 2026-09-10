# Tasks: Ajustar lógica de ucRegistroCertificadoCalidadCarburantes

## Review Workload Forecast

| Field | Value |
|-------|-------|
| Estimated changed lines | ~586 (140 additions + 446 deletions) |
| 400-line budget risk | High |
| Chained PRs recommended | Yes |
| Suggested split | PR 1: Cleanup (dead code + logging) → PR 2: Refactor (extractions + FormatHelper) |
| Delivery strategy | ask-on-risk |
| Chain strategy | pending |

Decision needed before apply: Yes
Chained PRs recommended: Yes
Chain strategy: pending
400-line budget risk: High

### Suggested Work Units

| Unit | Goal | Likely PR | Notes |
|------|------|-----------|-------|
| 1 | Remove dead code + add logging in 5 catch blocks | PR 1 | ~371 lines; pure deletion + small inline logging; low cognitive load |
| 2 | Extract 4 methods + create FormatHelper.cs | PR 2 | ~120 lines; depends on PR 1 for clean baseline; functional refactor |

## Phase 1: Foundation

- [x] 1.1 Create `Clases/VolumenesCalidad/FormatHelper.cs` with `ToDecimal(string)` culture-aware parser
- [x] 1.2 Add `BindearCombosGrilla(int, List<E_TABLA_ESPECIFICA>)` private method to code-behind

## Phase 2: Core Refactor

- [x] 2.1 Extract `ValidarIndiceCetano(...)` from grilla inline validation (Cetano 133/134 cross-check)
- [x] 2.2 Extract `ObtenerEspecificacionPorUnidad(string valor, string unidad, bool esMaxima)` from temperature-unit inline if-else (~6 sites)
- [x] 2.3 Replace all inline `Convert.ToDecimal(cond ? texto.Replace(".", ",") : ...)` with `FormatHelper.ToDecimal(texto)`

## Phase 3: Cleanup

- [x] 3.1 Remove all commented dead code (~366 lines) — verify no active references via reverse grep
- [x] 3.2 Remove commented-out `grdCertificadoCalidad_HtmlRowCreated` handler (~200 lines)

## Phase 4: Logging

- [x] 4.1 Add `ValidacionAdministradorHydro(ex, ...)` to catch at line 736 (`CargarOpcionAdjuntar`)
- [x] 4.2 Add logging to catch at line 2704 (`btnEliminarRegistroCalidad_Click`) — keep existing alert
- [x] 4.3 Add logging to catch at line 2878 (internal method)
- [x] 4.4 Add logging to catch at line 2913 (`ckbxVolumenCero_CheckedChanged`)
- [x] 4.5 Add logging to catch at line 2983 (`btnRegistrarVolumenCero_Click`)

## Phase 5: Verification

- [x] 5.1 Build solution (`msbuild AnhOctanoV2.sln`) — zero CS compilation errors
- [x] 5.2 Reverse grep all removed identifiers — no broken external references
- [ ] 5.3 Manual regression: render 3 host pages, verify grilla, guardado, Cetano validation identical
