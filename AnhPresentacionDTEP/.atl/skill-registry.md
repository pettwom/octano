# Skill Registry — AnhHydroOctanoCalidad (AnhPresentacionDTEP)

> Generated: 2026-05-21 by `sdd-init` | Mode: `engram`
> Project root: `H:\Sis2025\AnhHydroOctanoCalidad`

## Convention Files

| File | Path | Status |
|------|------|--------|
| `AGENTS.md` | `H:\Sis2025\AnhHydroOctanoCalidad\C:\Users\lufernandez\.config\opencode\AGENTS.md` | Loaded (system-level) |
| `CLAUDE.md` | — | Not found |
| `.cursorrules` | — | Not found |
| `GEMINI.md` | — | Not found |
| `copilot-instructions.md` | — | Not found |

## User-Level Skills (opencode)

_Source: `~/.config/opencode/skills/`_

| Skill | Trigger | Path | Rules |
|-------|---------|------|-------|
| `branch-pr` | Creating, opening, or preparing PRs for review | `~/.config/opencode/skills/branch-pr/SKILL.md` | • Always run issue-first checks before creating PR • Verify issue exists and is assigned • Draft PR with summary, context, and test plan • Ask at most one question at a time • Return PR URL when done |
| `chained-pr` | PRs over 400 lines, stacked PRs, review slices | `~/.config/opencode/skills/chained-pr/SKILL.md` | • Split oversized changes into ≤400-line reviewable slices • Each slice must be independently reviewable and mergeable • Order slices logically (deps first) • Each PR references the chain in its description |
| `cognitive-doc-design` | Writing guides, READMEs, RFCs, architecture docs | `~/.config/opencode/skills/cognitive-doc-design/SKILL.md` | • Focus on reducing reader cognitive load • Use hierarchical structure with clear sections • Keep paragraphs under 5 sentences • Use examples for complex concepts • Avoid walls of text |
| `comment-writer` | PR feedback, issue replies, reviews, Slack, GitHub comments | `~/.config/opencode/skills/comment-writer/SKILL.md` | • Be warm and direct • Validate the intent before criticizing • Explain WHY something is wrong, not just WHAT • Suggest concrete alternatives • Match user's language and tone |
| `go-testing` | Go tests, go test coverage, Bubbletea teatest | `~/.config/opencode/skills/go-testing/SKILL.md` | • N/A: project is .NET, not Go |
| `issue-creation` | Creating GitHub issues, bug reports, feature requests | `~/.config/opencode/skills/issue-creation/SKILL.md` | • Always check if similar issue exists before creating • Include reproduction steps for bugs • Describe expected vs actual behavior • Tag with appropriate labels • Use issue templates when available |
| `judgment-day` | Dual review, adversarial review, juzgar | `~/.config/opencode/skills/judgment-day/SKILL.md` | • Run blind dual review of the code • Fix all confirmed issues • Re-judge after fixes • Document what was found and fixed |
| `work-unit-commits` | Implementation, commit splitting, chained PRs | `~/.config/opencode/skills/work-unit-commits/SKILL.md` | • Plan commits as reviewable work units • Keep tests and docs with their code in same commit • Each commit must be independently reviewable • Split logically by feature/concern, not by file type |

## Project-Level Skills

_None detected. No `.claude/skills/`, `.agent/skills/`, `skills/`, or `.gemini/skills/` directory found under project root._

## SDD Skills (loaded via orchestration)

SDD lifecycle skills (`sdd-propose`, `sdd-spec`, `sdd-design`, `sdd-tasks`, `sdd-apply`, `sdd-verify`, `sdd-archive`, `sdd-onboard`, `sdd-explore`, `sdd-init`) are available through the opencode orchestration layer at `~/.config/opencode/skills/sdd-*`. These are not listed in detail here as they are invoked by the orchestrator, not directly by the user.

## Notes

- All user skills from `~/.config/opencode/skills/` are accessible to the agent in this session.
- No project-specific skills were found — consider creating project-level skills if project-specific conventions emerge.
- The `go-testing` skill is irrelevant for this .NET Framework project.
