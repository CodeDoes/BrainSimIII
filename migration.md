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
- **Avalonia:** `App.axaml`, `App.axaml.cs`, `Program.cs`
- **Migration:**
  - Move application startup logic to `Program.cs`.
  - Update resource loading and initialization.

## 4. Main Window
- **WPF:** `MainWindow.xaml`, `MainWindow.xaml.cs`
- **Avalonia:** `MainWindow.axaml`, `MainWindow.axaml.cs`
- **Migration:**
  - Recreate UI layout in Avalonia AXAML.
  - Update event handlers and data bindings.

## 5. Modules and Logic
- **Shared Logic:** Move non-UI logic (e.g., modules in `Modules/`, `Network.cs`, `Utils.cs`) to a shared library if possible.
- **UI-Dependent Logic:** Refactor to use Avalonia controls and patterns.

## 6. Resources and Assets
- **Images, Data Files:** Update resource references to Avalonia conventions.
- **Resource Dictionaries:** Convert WPF resource dictionaries to Avalonia styles.

## 7. Platform Differences
- **.NET Version:** Ensure all projects target compatible .NET versions (prefer .NET 6+ for Avalonia).
- **NuGet Packages:** Replace WPF-specific packages with Avalonia equivalents.

## 8. Testing
- **Manual Testing:** Validate UI and features in Avalonia app.
- **Automated Testing:** Update or create new tests for Avalonia project.

## 9. Documentation
- Update README and documentation to reflect Avalonia usage and new build/run instructions.

---

## Migration Checklist (File-by-File)

### Project Setup
- [ ] Set up Avalonia project (`BrainSimulatorAvalonia/`)
- [ ] Migrate project file: `BrainSimulator.csproj`
- [ ] Update assembly info: `AssemblyInfo.cs`
- [ ] Update launch settings: `Properties/launchSettings.json`

### Application Entry & Main Window
 - [x] App.xaml → App.axaml
 - [x] App.xaml.cs → App.axaml.cs
 - [x] MainWindow.xaml → MainWindow.axaml
 - [ ] MainWindow.xaml.cs → MainWindow.axaml.cs
 - [ ] Program.cs (Avalonia entry point)

### Main Window Logic
 - [ ] MainWindowEventHandlers.cs
 - [ ] MainWindowFiles.cs
 - [ ] MainWindowPythonModules.cs

### Dialogs & Descriptions
 - [ ] ModuleDescription.xaml → ModuleDescription.axaml
 - [ ] ModuleDescription.xaml.cs → ModuleDescription.axaml.cs
 - [ ] NotesDialog.xaml → NotesDialog.axaml
 - [ ] NotesDialog.xaml.cs → NotesDialog.axaml.cs

### Core Logic & Utilities
- [ ] Network.cs
- [ ] Utils.cs
- [ ] XmlFile.cs
- [ ] ModuleHandler.cs
- [ ] ModuleViewMenu.cs

### Modules (Code & Dialogs)
#### For each module, migrate both code and dialog files:
- [ ] Modules/ModuleBase.cs
- [ ] Modules/ModuleBaseDlg.cs
- [ ] Modules/ModuleEmpty.cs
- [ ] Modules/ModuleEmptyDlg.xaml → ModuleEmptyDlg.axaml
- [ ] Modules/ModuleEmptyDlg.xaml.cs → ModuleEmptyDlg.axaml.cs
- [ ] Modules/ModuleGPTInfo.cs
- [ ] Modules/ModuleGPTInfoDlg.xaml → ModuleGPTInfoDlg.axaml
- [ ] Modules/ModuleGPTInfoDlg.xaml.cs → ModuleGPTInfoDlg.axaml.cs
- [ ] Modules/ModuleMine.cs
- [ ] Modules/ModuleOnlineInfo.cs
- [ ] Modules/ModuleOnlineInfoDlg.xaml → ModuleOnlineInfoDlg.axaml
- [ ] Modules/ModuleOnlineInfoDlg.xaml.cs → ModuleOnlineInfoDlg.axaml.cs
- [ ] Modules/ModuleShowGraph.cs
- [ ] Modules/ModuleShowGraphDlg.xaml → ModuleShowGraphDlg.axaml
- [ ] Modules/ModuleShowGraphDlg.xaml.cs → ModuleShowGraphDlg.axaml.cs
- [ ] Modules/ModuleStressTest.cs
- [ ] Modules/ModuleStressTestDlg.xaml → ModuleStressTestDlg.axaml
- [ ] Modules/ModuleStressTestDlg.xaml.cs → ModuleStressTestDlg.axaml.cs
- [ ] Modules/ModuleTextFile.cs
- [ ] Modules/ModuleTextFileDlg.xaml → ModuleTextFileDlg.axaml
- [ ] Modules/ModuleTextFileDlg.xaml.cs → ModuleTextFileDlg.axaml.cs
- [ ] Modules/ModuleUKS.cs
- [ ] Modules/ModuleUKSClause.cs
- [ ] Modules/ModuleUKSClauseDlg.xaml → ModuleUKSClauseDlg.axaml
- [ ] Modules/ModuleUKSClauseDlg.xaml.cs → ModuleUKSClauseDlg.axaml.cs
- [ ] Modules/ModuleUKSDlg.xaml → ModuleUKSDlg.axaml
- [ ] Modules/ModuleUKSDlg.xaml.cs → ModuleUKSDlg.axaml.cs
- [ ] Modules/ModuleUKSQuery.cs
- [ ] Modules/ModuleUKSQueryDlg.xaml → ModuleUKSQueryDlg.axaml
- [ ] Modules/ModuleUKSQueryDlg.xaml.cs → ModuleUKSQueryDlg.axaml.cs
- [ ] Modules/ModuleUKSStatement.cs
- [ ] Modules/ModuleUKSStatementDlg.xaml → ModuleUKSStatementDlg.axaml
- [ ] Modules/ModuleUKSStatementDlg.xaml.cs → ModuleUKSStatementDlg.axaml.cs
- [ ] Modules/PointPlus.cs

### Resources & Assets
- [ ] Resources/ (all images, icons, etc.)
- [ ] Finetuning/ (all .jsonl and .txt files)
- [ ] UKSContent/ (all .xml files)
- [ ] WordFIles/ (all .txt and .xml files)
- [ ] Tools/ (templates, icons, etc.)

### Configuration & Documentation
- [ ] ModuleDescriptions.xml
- [ ] Doxyfile
- [ ] README.md
- [ ] LICENSE

For detailed migration steps, refer to the [Avalonia documentation](https://docs.avaloniaui.net/).

---

## Migration Quality Notes

- **No placeholders/stubs:** All migrated files should contain real, functional Avalonia code, not just stubs or placeholders. If a file was migrated with placeholder logic, it must be revisited and completed.
- **Revisit List:**
  - [ ] ModuleDescription.axaml.cs (replace stub logic with real Avalonia implementation)
  - [ ] NotesDialog.axaml.cs (replace stub logic with real Avalonia implementation)
  - [ ] Any other files migrated with placeholders

Update this section as files are fully migrated with real logic.
