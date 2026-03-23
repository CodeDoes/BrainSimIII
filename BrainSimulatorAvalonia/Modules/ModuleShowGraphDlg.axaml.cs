// PROPRIETARY AND CONFIDENTIAL
// Brain Simulator 3 v.1.0
// © 2022 FutureAI, Inc., all rights reserved

using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using System;
using UKS;

namespace BrainSimulatorAvalonia.Modules
{
    public partial class ModuleShowGraphDlg : ModuleBaseDlg
    {
        public ModuleShowGraphDlg()
        {
            InitializeComponent();
        }

        public override bool Draw(bool checkDrawTimer)
        {
            if (!base.Draw(checkDrawTimer)) return false;
            // TODO: Implement graph drawing using Avalonia controls or a compatible graph library
            // Placeholder: set background to black
            if (this.FindControl<Border>("GraphHost") is Border graphHost)
                graphHost.Background = Brushes.Black;
            return true;
        }

        private void ButtonRefresh_Click(object? sender, RoutedEventArgs e)
        {
            Draw(false);
        }
    }
}
