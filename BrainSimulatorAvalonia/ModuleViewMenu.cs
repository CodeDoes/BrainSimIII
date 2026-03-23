using BrainSimulator.Modules;
using System;
using System.Diagnostics;
using System.IO;
// using Avalonia.Controls; // TODO: Use Avalonia UI controls
// using Avalonia.Input;
// using Avalonia.Media;

namespace BrainSimulatorAvalonia
{
    // NOTE: This is a partial migration. All WPF-specific UI code is omitted or marked as TODO.
    public partial class MainWindow // : Window (Avalonia)
    {
        // TODO: Implement Avalonia equivalent for moduleNameProperty
        // public static readonly StyledProperty<string> ModuleNameProperty = ...;

        // TODO: Port CreateContextMenu to Avalonia (ContextMenu, MenuItem, event handlers, etc.)
        public void CreateContextMenu(ModuleBase nr, object r, object cm = null)
        {
            // All WPF UI logic omitted. Needs Avalonia implementation.
        }

        // TODO: Port Cb1_Checked event handler to Avalonia
        private void Cb1_Checked(object sender, EventArgs e)
        {
            // Not implemented
        }

        // TODO: Port B0_Click event handler to Avalonia
        private void B0_Click(object sender, EventArgs e)
        {
            // Not implemented
        }

        // TODO: Port Cm_Closed event handler to Avalonia
        private void Cm_Closed(object sender, EventArgs e)
        {
            // Not implemented
        }

        // TODO: Port Mi_Click event handler to Avalonia
        private void Mi_Click(object sender, EventArgs e)
        {
            // Not implemented
        }

        // TODO: Port OpenSource to Avalonia (external process launch)
        public static void OpenSource(string fileName)
        {
            // Not implemented
        }

        // TODO: Port DeleteModule to Avalonia
        public void DeleteModule(string moduleName)
        {
            // Not implemented
        }
    }
}
