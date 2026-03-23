// Copyright (c) FutureAI. All rights reserved.  
// Contains confidential and proprietary information and programs which may not be distributed without a separate license
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using UKS;

namespace BrainSimulatorAvalonia.Modules
{
    public partial class ModuleGPTInfoDlg : ModuleBaseDlg
    {
        // Error count and relationship count, used for debugging (static for now, working on fix).
        public static int errorCount;
        public static int relationshipCount;

        // Word max set to 100 by default. *Modify/Increase Value at your own risk!*
        int wordMax = 100;
        List<string> words = new List<string>();  // List to hold all words

        // UKS call
        // Needed when accessing all items in the UKS.
        public static ModuleHandler moduleHandler = new();
        public static UKS.UKS theUKS = moduleHandler.theUKS;

        public ModuleGPTInfoDlg()
        {
            InitializeComponent();
        }

        public override bool Draw(bool checkDrawTimer)
        {
            if (this.FindControl<Label>("StatusLabel") is Label statusLabel)
            {
                var mf = (ModuleGPTInfo)base.ParentModule;
                statusLabel.Content = $"{GPT.totalTokensUsed} tokens used.  {mf.theUKS.Labeled(\"unknownObject\")?.Children.Count} unknown Things.  ";
            }
            return base.Draw(checkDrawTimer);
        }

        private void SetOutputText(string theText)
        {
            if (this.FindControl<TextBox>("txtOutput") is TextBox txtOutput)
                txtOutput.Text = theText;
            if (this.FindControl<Label>("StatusLabel") is Label statusLabel)
            {
                var mf = (ModuleGPTInfo)base.ParentModule;
                statusLabel.Content = $"{GPT.totalTokensUsed} tokens used.  {mf.theUKS.Labeled(\"unknownObject\")?.Children.Count} unknown Things.  ";
            }
        }

        private void LoadButton_Click(object? sender, RoutedEventArgs e)
        {
            // TODO: Implement logic for loading word file
        }

        private void textInput_KeyDown(object? sender, Avalonia.Input.KeyEventArgs e)
        {
            // TODO: Implement logic for handling key down in textInput
        }

        private void ReParseButton_Click(object? sender, RoutedEventArgs e)
        {
            // TODO: Implement logic for re-parsing
        }

        private void HandleUnknownsButton_Click(object? sender, RoutedEventArgs e)
        {
            // TODO: Implement logic for handling unknowns
        }

        private void VerifyAllParentsButton_Click(object? sender, RoutedEventArgs e)
        {
            // TODO: Implement logic for verifying all parents
        }

        private void AddClausesToAllButton_Click(object? sender, RoutedEventArgs e)
        {
            // TODO: Implement logic for adding clauses to all
        }

        private void SolveAmbiguityButton_Click(object? sender, RoutedEventArgs e)
        {
            // TODO: Implement logic for solving ambiguity
        }

        private void AmbiguityFileButton_Click(object? sender, RoutedEventArgs e)
        {
            // TODO: Implement logic for ambiguity file
        }

        private void RemoveDuplicatesButton_Click(object? sender, RoutedEventArgs e)
        {
            // TODO: Implement logic for removing duplicates
        }
        // If there are any additional event handlers, implement them here.
    }
}
