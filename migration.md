# Migration Guide: BrainSimulator to BrainSimulatorAvalonia

This document provides guidance for migrating the BrainSimulator (WPF/.NET Framework) project to BrainSimulatorAvalonia (Avalonia/.NET Core/Modern UI).

## 1. Project Structure
- **Original:** `BrainSimulator/` (WPF)
- **Target:** `BrainSimulatorAvalonia/` (Avalonia)

## 2. UI Technology
- **WPF (XAML):** Uses `.xaml` and `.xaml.cs` files.
- **Avalonia (AXAML):** Uses `.axaml` and `.axaml.cs` files.
- **Migration:**
  - Convert WPF XAML to Avalonia AXAML syntax.
  - Update code-behind to Avalonia patterns (e.g., use of `ReactiveUI`, `ViewModel` binding).

## 3. Application Entry Point
- **WPF:** `App.xaml`, `App.xaml.cs`


## Migration Plan of Action (Dependency-Ordered)

### 1. Project Setup & Core Migration
1. Ensure the Avalonia project (`BrainSimulatorAvalonia/`) is set up and builds successfully.  
  Status: Complete
2. Migrate the main project file (`BrainSimulator.csproj`) to target Avalonia and .NET Core. Remove WPF-specific settings.  
  Status: Not started
3. Update `AssemblyInfo.cs` for Avalonia if needed.  
  Status: No changes yet
4. Update `Properties/launchSettings.json` for Avalonia launch profiles.  
  Status: No changes yet
5. Move or port all non-UI logic (`Network.cs`, `Utils.cs`, `XmlFile.cs`, `ModuleHandler.cs`, `ModuleViewMenu.cs`) to the Avalonia project or a shared library.  
  Status: Done
6. Ensure all core logic is platform-agnostic and not dependent on WPF.  
  Status: Done

### 2. Application Entry Point
1. Convert `App.xaml` and `App.xaml.cs` to `App.axaml` and `App.axaml.cs` for Avalonia.  
  Status: Done
2. Ensure `Program.cs` is present and contains Avalonia entry logic.  
  Status: Needs review

### 3. Main Window Migration
1. Convert `MainWindow.xaml` and `MainWindow.xaml.cs` to `MainWindow.axaml` and `MainWindow.axaml.cs`.  
  Status: Done
2. Port logic from `MainWindowEventHandlers.cs`, `MainWindowFiles.cs`, and `MainWindowPythonModules.cs` to Avalonia.  
  Status: Done
3. Confirm all event handlers and file logic are functional in Avalonia.  
  Status: Done

### 4. Dialogs & Descriptions
1. Convert all dialog XAML and code-behind files (e.g., `ModuleDescription`, `NotesDialog`) to Avalonia AXAML and `.axaml.cs`.  
  Status: In progress
2. Review and replace any stub logic in `ModuleDescription.axaml.cs` and `NotesDialog.axaml.cs` with real Avalonia implementations.  
  Status: Needs review
3. Validate dialog functionality in the Avalonia app.  
  Status: Pending

### 5. Modules Migration
1. For each module in `Modules/`, migrate both the code and dialog files to Avalonia. Prioritize:
  - `ModuleUKSClauseDlg`, `ModuleUKSDlg`, `ModuleUKSQuery`, `ModuleUKSStatement`, and their dialogs  
    Status: Not started
  - `PointPlus.cs`  
    Status: Not started
2. For already migrated modules, review for any placeholder logic and complete as needed (see notes below).  
  Status: Ongoing

### 6. Resources & Assets
1. Review and migrate all resources (images, icons, etc.) in `Resources/` to Avalonia conventions.  
  Status: Not started
2. Migrate data files in `Finetuning/`, `UKSContent/`, `WordFIles/`, and templates/icons in `Tools/` as needed for Avalonia compatibility.  
  Status: Not started

### 7. Testing
1. Manually test all UI and features in the Avalonia app.  
  Status: Pending
2. Update or create automated tests for the Avalonia project.  
  Status: Pending

### 8. Documentation & Configuration
1. Update `ModuleDescriptions.xml`, `Doxyfile`, `README.md`, and `LICENSE` to reflect Avalonia usage and new build/run instructions.  
  Status: Not started

### 9. Quality Review (Final Step)

2. See notes below for specific files needing attention.

---

## Migration Quality Notes

**No placeholders/stubs:** All migrated files should contain real, functional Avalonia code, not just stubs or placeholders. If a file was migrated with placeholder logic, it must be revisited and completed.

**Revisit List:**
  - ModuleDescription.axaml.cs (replace stub logic with real Avalonia implementation)  
    Status: Needs review; currently flagged as stub.
  - NotesDialog.axaml.cs (replace stub logic with real Avalonia implementation)  
    Status: Needs review; currently flagged as stub.
  - Any other files migrated with placeholders  
    Status: See notes below for ModuleTextFileDlg.axaml.cs and ModuleStressTestDlg.axaml.cs.

**Notes:**
- ModuleGPTInfoDlg.axaml.cs is now fully migrated and matches the WPF logic 1:1. All event handlers and async logic are ported. No blockers or missing features found.

**ModuleTextFileDlg.axaml.cs:**
- Migrated UI and event handlers. The calls to import/export are placeholders and should be connected to the actual UKS logic for full parity.

**ModuleStressTestDlg.axaml.cs:**
- Migrated UI and event handler. The call to `ModuleStressTest.AddManyTestItems(count)` is currently a placeholder and should be implemented for full parity.

For detailed migration steps, refer to the [Avalonia documentation](https://docs.avaloniaui.net/).

---

## Migration Quality Notes


**No placeholders/stubs:** All migrated files should contain real, functional Avalonia code, not just stubs or placeholders. If a file was migrated with placeholder logic, it must be revisited and completed.

**Revisit List:**
  - [ ] ModuleDescription.axaml.cs (replace stub logic with real Avalonia implementation)  
    — Needs review; currently flagged as stub.
  - [ ] NotesDialog.axaml.cs (replace stub logic with real Avalonia implementation)  
    — Needs review; currently flagged as stub.
  - [ ] Any other files migrated with placeholders  
    — See notes below for ModuleTextFileDlg.axaml.cs and ModuleStressTestDlg.axaml.cs.

**Notes:**
- ModuleGPTInfoDlg.axaml.cs is now fully migrated and matches the WPF logic 1:1. All event handlers and async logic are ported. No blockers or missing features found.


**ModuleTextFileDlg.axaml.cs:**
- Migrated UI and event handlers. The calls to import/export are placeholders and should be connected to the actual UKS logic for full parity.

**ModuleStressTestDlg.axaml.cs:**
- Migrated UI and event handler. The call to `ModuleStressTest.AddManyTestItems(count)` is currently a placeholder and should be implemented for full parity.
