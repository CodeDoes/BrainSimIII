using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Collections.Generic;
using System.IO;
using System;

namespace BrainSimulatorAvalonia.Modules
{
    public partial class ModuleDescriptionDlg : Window
    {
        string moduleType = "";
        public ModuleDescriptionDlg(string theModuleType)
        {
            InitializeComponent();
            moduleType = theModuleType;
            // TODO: Replace with Avalonia equivalent for getting module types
            var modules = Utils.GetListOfExistingCSharpModuleTypes();
            foreach (var v in modules)
            {
                moduleSelector.Items = modules;
            }
            moduleSelector.SelectedItem = theModuleType.Replace("Module", "");
        }

        private void buttonSave_Click(object? sender, RoutedEventArgs e)
        {
            ModuleDescriptionFile.SetDescription(moduleType, Description.Text);
            ModuleDescriptionFile.Save();
        }

        private void moduleSelector_SelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox cb && cb.SelectedItem != null)
            {
                moduleType = "Module" + cb.SelectedItem.ToString();
                Description.Text = ModuleDescriptionFile.GetDescription(moduleType);
            }
        }

        private void buttonClose_Click(object? sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

    public class ModuleDescriptionFile
    {
        public class ModuleDescription
        {
            public string moduleName;
            public string description;
        }
        public static List<ModuleDescription> theModuleDescriptions = null;
        // TODO: Implement SetDescription, Save, and GetDescription for Avalonia
    }
}
