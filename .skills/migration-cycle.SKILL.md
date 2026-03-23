# Migration Cycle Skill: 1-to-1 Legacy-to-Avalonia Migration

## Purpose
Automate and standardize the migration of modules/dialogs from a legacy BrainSimulator (WPF) project to an Avalonia (AXAML) project, ensuring 1:1 feature parity, documentation, and task tracking.

## Workflow Steps
1. **Identify Next Migration Task**
   - Read the migration checklist and todo list.
   - Select the next incomplete migration item.
2. **Compare Old and New Implementations**
   - Locate the legacy (WPF) and Avalonia (AXAML) files for the module/dialog.
   - Compare structure, logic, and UI to ensure 1:1 feature parity.
3. **Migrate or Update Avalonia Implementation**
   - If missing or incomplete, migrate code and UI from WPF to Avalonia.
   - Adapt to Avalonia patterns (controls, event handlers, async, etc.).
   - Note any features that cannot be migrated (e.g., unsupported controls).
4. **Mark as Complete**
   - Update the migration checklist and todo list to mark the item as complete.
   - Add notes to migration.md about any limitations or deviations.
5. **Commit Changes**
   - Commit all migration changes with a descriptive message.
6. **Repeat**
   - Move to the next migration task and repeat the cycle until all are complete.

## Quality Criteria
- Avalonia implementation matches WPF logic and UI as closely as possible.
- All migrated files are functional (no placeholders unless noted in migration.md).
- All changes are tracked in migration.md and the todo list.
- Commit messages are clear and reference the migration step.
- Notes are made for any blockers or unimplemented features.

## Example Prompts
- "Migrate the next module from WPF to Avalonia using the migration cycle skill."
- "Run the migration cycle for ModuleTextFileDlg.xaml."
- "Continue the migration cycle until all modules are complete."

## Related Customizations
- Add a checklist for post-migration manual testing.
- Create a skill for validating 1:1 UI/logic parity between WPF and Avalonia.
- Add a skill for batch updating migration.md and todo lists.

---

This skill ensures a repeatable, high-quality migration process for large multi-file projects moving from WPF to Avalonia.
