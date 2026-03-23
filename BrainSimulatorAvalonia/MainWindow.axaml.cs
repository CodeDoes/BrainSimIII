using System;
using System.Collections.Generic;
using BrainSimulatorAvalonia.Modules;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using System.Linq;

namespace BrainSimulatorAvalonia
{
    public partial class MainWindow : Window
    {
        public void CreateEmptyUKS()
        {
            theUKS.UKSList.Clear();
            theUKS = new UKS.UKS();
            theUKS.AddThing("BrainSim", null);
            theUKS.GetOrAddThing("AvailableModule", "BrainSim");
            theUKS.GetOrAddThing("ActiveModule", "BrainSim");

            InsertMandatoryModules();
            InitializeActiveModules();
        }

        public void UpdateModuleListsInUKS()
        {
            theUKS.GetOrAddThing("BrainSim", null);
            theUKS.GetOrAddThing("AvailableModule", "BrainSim");
            theUKS.GetOrAddThing("ActiveModule", "BrainSim");
            // Additional logic can be ported as needed
        }

        public void InsertMandatoryModules()
        {
            // Ported from WPF: Insert mandatory modules into the UKS
            ActivateModule("ModuleUKS");
            ActivateModule("ModuleUKSStatement");
        }

        public string ActivateModule(string moduleType)
        {
            var t = theUKS.GetOrAddThing(moduleType, "AvailableModule");
            t = theUKS.CreateInstanceOf(theUKS.Labeled(moduleType));
            t.AddParent(theUKS.Labeled("ActiveModule"));

            if (!moduleType.Contains(".py"))
            {
                ModuleBase newModule = CreateNewModule(moduleType);
                if (newModule == null) return "";
                newModule.Label = t.Label;
                activeModules.Add(newModule);
            }
            return t.Label;
        }
        // Add missing Dt_Tick method for DispatcherTimer
        private void Dt_Tick(object? sender, EventArgs e)
        {
            var activeModuleParent = theUKS.Labeled("ActiveModule");
            if (activeModuleParent == null) return;
            foreach (var module in activeModuleParent.Children)
            {
                var mb = activeModules.FirstOrDefault(x => x.Label == module.Label);
                if (mb != null)
                {
                    mb.Fire();
                }
            }
            foreach (string pythonModule in pythonModules)
            {
                moduleHandler.RunScript(pythonModule);
            }
        }
        // Ported fields from WPF
        public List<ModuleBase> activeModules = new();
        public List<string> pythonModules = new();
        public static string currentFileName = "";
        public static string pythonPath = "";
        public static ModuleHandler moduleHandler = new();
        public static UKS.UKS theUKS = moduleHandler.theUKS;
        public static MainWindow theWindow = null;

        // Stub for CreateNewModule to allow build to proceed
        public ModuleBase CreateNewModule(string moduleTypeLabel, string moduleLabel = "")
        {
            // Try to get the type from the Avalonia modules namespace
            var typeName = "BrainSimulatorAvalonia.Modules." + moduleTypeLabel;
            var t = Type.GetType(typeName);
            if (t == null)
                return null;
            ModuleBase theModule = (Modules.ModuleBase)Activator.CreateInstance(t);
            theModule.Label = string.IsNullOrEmpty(moduleLabel) ? moduleTypeLabel : moduleLabel;
            // Optionally: theModule.GetUKS(); // if needed for Avalonia
            return theModule;
        }


        public MainWindow()
        {
            InitializeComponent();
            theWindow = this;
            SetTitleBar();
            this.Opened += MainWindow_Opened;
        }


        private async void MainWindow_Opened(object? sender, EventArgs e)
        {
            // Ported logic from WPF MainWindow_Loaded
            pythonPath = Environment.GetEnvironmentVariable("PythonPath", EnvironmentVariableTarget.User);
            if (string.IsNullOrEmpty(pythonPath))
            {
                // TODO: Port dialog for Python selection (Avalonia equivalent)
                pythonPath = "no";
                Environment.SetEnvironmentVariable("PythonPath", pythonPath, EnvironmentVariableTarget.User);
            }
            moduleHandler.PythonPath = pythonPath;
            if (pythonPath != "no")
            {
                moduleHandler.InitPythonEngine();
            }
            // Ported file loading logic from WPF
            string fileName = "";
            // TODO: Replace with Avalonia settings storage if needed
            string savedFile = ""; // e.g., load from a config file or user settings
            if (!string.IsNullOrEmpty(savedFile))
                fileName = savedFile;

            try
            {
                if (!string.IsNullOrEmpty(fileName))
                {
                    if (!LoadFile(fileName))
                    {
                        ShowMessage("Previous UKS File could not be opened, empty UKS initialized", "File not read");
                        CreateEmptyUKS();
                    }
                }
                else //force a new file creation on startup if no file name set
                {
                    CreateEmptyUKS();
                }
            }
            catch (Exception)
            {
                ShowMessage("UKS Content not loaded");
            }

            //safety check
            if (theUKS.Labeled("BrainSim") == null)
                CreateEmptyUKS();

            UpdateModuleListsInUKS();
            // TODO: Port LoadModuleTypeMenu and LoadMRUMenu if needed
            InitializeActiveModules();

            // Start DispatcherTimer for module engine
            var timer = new Avalonia.Threading.DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(0.1)
            };
            timer.Tick += Dt_Tick;
            timer.Start();
        }

        // Stub for LoadFile - should be implemented to match WPF logic
        // This is a placeholder for future file loading implementation.
        private bool LoadFile(string fileName)
        {
            // Ported from WPF: Load UKS file and initialize modules
            // Suspend engine, close dialogs, unload modules (stubs if not implemented)
            // SuspendEngine();
            // CloseAllModuleDialogs();
            // UnloadActiveModules();

            if (!theUKS.LoadUKSfromXMLFile(fileName))
            {
                theUKS = new UKS.UKS();
                return false;
            }
            currentFileName = fileName;

            if (theUKS.Labeled("BrainSim") == null)
                CreateEmptyUKS();

            // SetCurrentFileNameToProperties(); // stub or implement as needed
            UpdateModuleListsInUKS();
            // LoadActiveModules(); // stub or implement as needed
            // ReloadActiveModulesSP(); // stub or implement as needed
            // ShowAllModuleDialogs(); // stub or implement as needed
            SetTitleBar();
            // ResumeEngine();
            // AddFileToMRUList(fileName); // stub or implement as needed
            return true;
        }

        // Show a simple message box (Avalonia equivalent)
        async void ShowMessage(string message, string title = "Info")
        {
            var dialog = new Window
            {
                Title = title,
                Width = 400,
                Height = 150,
                Content = new StackPanel
                {
                    Children =
                    {
                        new TextBlock { Text = message, Margin = new Thickness(10) },
                        new Button { Content = "OK", HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center, Margin = new Thickness(10) }
                    }
                }
            };
            await dialog.ShowDialog(this);
        }

        // --- Stubs for methods to be ported ---
        public void InitializeActiveModules()
        {
            for (int i = 0; i < activeModules.Count; i++)
            {
                ModuleBase mod = activeModules[i];
                if (mod != null)
                {
                    mod.SetUpAfterLoad();
                }
            }
        }
        private void SetTitleBar() { this.Title = "Brain Simulator III " + System.IO.Path.GetFileNameWithoutExtension(currentFileName); }
        public static void SuspendEngine() { /* TODO: Port logic */ }
        public static void ResumeEngine() { /* TODO: Port logic */ }
        // Ported DispatcherTimer logic above. Next: Port LoadModuleTypeMenu logic for Avalonia ComboBox.

        // Example: Port LoadModuleTypeMenu (stub, needs ComboBox reference from XAML)
        // private void LoadModuleTypeMenu()
        // {
        //     var moduleTypes = Utils.GetListOfExistingCSharpModuleTypes();
        //     // TODO: Get ComboBox reference from XAML (e.g., this.FindControl<ComboBox>("ModuleListComboBox"))
        //     // Populate ComboBox.Items with module names
        // }

        // Adapt dialog logic: use ShowOpenFileDialogAsync and ShowSaveFileDialogAsync for file dialogs

        // Adapt message dialogs: ShowMessage already uses Avalonia Window

        async System.Threading.Tasks.Task<string?> ShowOpenFileDialogAsync()
        {
            var dialog = new OpenFileDialog
            {
                Title = "Open File"
            };
            var result = await dialog.ShowAsync(this);
            return result != null && result.Length > 0 ? result[0] : null;
        }

        async System.Threading.Tasks.Task<string?> ShowSaveFileDialogAsync()
        {
            var dialog = new SaveFileDialog
            {
                Title = "Save File"
            };
            var result = await dialog.ShowAsync(this);
            return result;
        }

        // TODO: Port all other methods from WPF MainWindow.xaml.cs
    }
}
