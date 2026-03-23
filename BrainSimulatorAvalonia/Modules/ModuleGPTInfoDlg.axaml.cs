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
                statusLabel.Content = $"{GPT.totalTokensUsed} tokens used.  {mf.theUKS.Labeled("unknownObject")?.Children.Count} unknown Things.  ";
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
                statusLabel.Content = $"{GPT.totalTokensUsed} tokens used.  {mf.theUKS.Labeled("unknownObject")?.Children.Count} unknown Things.  ";
            }
        }

        private async void LoadButton_Click(object? sender, RoutedEventArgs e)
        {
            errorCount = 0;
            relationshipCount = 0;
            words.Clear();
            var dialog = new OpenFileDialog();
            dialog.Title = "Select a file";
            dialog.Filters.Add(new FileDialogFilter() { Name = "Text files", Extensions = { "txt" } });
            dialog.Filters.Add(new FileDialogFilter() { Name = "All files", Extensions = { "*" } });
            var result = await dialog.ShowAsync(this.GetWindow());
            if (result != null && result.Length > 0)
            {
                string filePath = result[0];
                try
                {
                    var pluralizer = new Pluralize.NET.Pluralizer();
                    using (var reader = new System.IO.StreamReader(filePath))
                    {
                        string? line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            if (words.Count >= wordMax) break;
                            var lineWords = line.Split(new char[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                            for (int i = 0; i < lineWords.Count; i++)
                            {
                                if (lineWords[i].Trim() == "")
                                {
                                    lineWords.RemoveAt(i);
                                    i--;
                                }
                                lineWords[i] = pluralizer.Singularize(lineWords[i]);
                            }
                            words.AddRange(lineWords);
                        }
                    }
                    if (this.FindControl<TextBox>("txtOutput") is TextBox txtOutput)
                        txtOutput.Text = "File Successfully Read!";
                }
                catch (Exception error)
                {
                    SetOutputText("An error occurred while reading the file:\n" + error.Message);
                }
            }
            else
            {
                if (this.FindControl<TextBox>("txtOutput") is TextBox txtOutput)
                    txtOutput.Text = "No file selected.";
            }
            await ProcessWordsAsync(words);
        }

        private async void textInput_KeyDown(object? sender, Avalonia.Input.KeyEventArgs e)
        {
            if (sender is TextBox textInput)
            {
                if (e.Key == Avalonia.Input.Key.Enter)
                {
                    SetOutputText("Working...");
                    string txt = textInput.Text;
                    await ModuleGPTInfo.GetChatGPTData(txt);
                    SetOutputText(ModuleGPTInfo.Output);
                    if (this.FindControl<TextBox>("txtOutput") is TextBox txtOutput)
                        txtOutput.Text += $"\n\rTotal relationship count: {relationshipCount}. Total error count (not accepted): {errorCount}.";
                }
                if (e.Key == Avalonia.Input.Key.Up)
                {
                    SetOutputText("Working...");
                    string txt = textInput.Text;
                    await ModuleGPTInfo.GetChatGPTParents(txt);
                    SetOutputText(ModuleGPTInfo.Output);
                }
            }
        }

        private void ReParseButton_Click(object? sender, RoutedEventArgs e)
        {
            if (this.FindControl<TextBox>("textInput") is TextBox textInput && this.FindControl<TextBox>("txtOutput") is TextBox txtOutput)
            {
                ModuleGPTInfo.ParseGPTOutput(textInput.Text, txtOutput.Text);
            }
        }

        private void HandleUnknownsButton_Click(object? sender, RoutedEventArgs e)
        {
            var mf = (ModuleGPTInfo)base.ParentModule;
            words.Clear();
            var thingList = mf.theUKS.Labeled("unknownObject").Children;
            SetOutputText($"Getting parents for {thingList.Count} Things");
            foreach (var thing in thingList)
            {
                if (words.Count >= wordMax) break;
                words.Add(thing.Label);
            }
            ProcessParentsAsync(words);
        }

        private void VerifyAllParentsButton_Click(object? sender, RoutedEventArgs e)
        {
            verifyAllAsync();
        }

        private void AddClausesToAllButton_Click(object? sender, RoutedEventArgs e)
        {
            GetGPTClausesAsync();
        }

        private void SolveAmbiguityButton_Click(object? sender, RoutedEventArgs e)
        {
            SolveAmbiguityGPTAsync();
        }

        private async void AmbiguityFileButton_Click(object? sender, RoutedEventArgs e)
        {
            errorCount = 0;
            relationshipCount = 0;
            words.Clear();
            var dialog = new OpenFileDialog();
            dialog.Title = "Select a file";
            dialog.Filters.Add(new FileDialogFilter() { Name = "Text files", Extensions = { "txt" } });
            dialog.Filters.Add(new FileDialogFilter() { Name = "All files", Extensions = { "*" } });
            var result = await dialog.ShowAsync(this.GetWindow());
            if (result != null && result.Length > 0)
            {
                string filePath = result[0];
                try
                {
                    var pluralizer = new Pluralize.NET.Pluralizer();
                    using (var reader = new System.IO.StreamReader(filePath))
                    {
                        string? line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            if (words.Count >= wordMax) break;
                            var lineWords = line.Split(new char[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                            for (int i = 0; i < lineWords.Count; i++)
                            {
                                if (lineWords[i].Trim() == "")
                                {
                                    lineWords.RemoveAt(i);
                                    i--;
                                }
                                lineWords[i] = pluralizer.Singularize(lineWords[i]);
                            }
                            words.AddRange(lineWords);
                        }
                    }
                    if (this.FindControl<TextBox>("txtOutput") is TextBox txtOutput)
                        txtOutput.Text = "File Successfully Read!";
                }
                catch (Exception error)
                {
                    SetOutputText("An error occurred while reading the file:\n" + error.Message);
                }
            }
            else
            {
                if (this.FindControl<TextBox>("txtOutput") is TextBox txtOutput)
                    txtOutput.Text = "No file selected.";
            }
            await ProcessAmbiguityAsync(words);
        }

        private void RemoveDuplicatesButton_Click(object? sender, RoutedEventArgs e)
        {
            SolveDuplicatesAsync();
        }

        // --- Async logic ported from WPF ---
        public async Task ProcessWordsAsync(List<string> words)
        {
            SetOutputText("Running through words... Word count is: " + words.Count + ".");
            foreach (string word in words)
            {
                if (word == words.Last())
                    await ModuleGPTInfo.GetChatGPTData(word.Trim());
                if (word.Trim() != "")
                    ModuleGPTInfo.GetChatGPTData(word.Trim());
            }
            if (this.FindControl<TextBox>("txtOutput") is TextBox txtOutput)
                txtOutput.Text = $"Done running! Total word count: {words.Count}. Total relationship count: {relationshipCount}. Total error count (not accepted): {errorCount}.";
        }

        public async Task ProcessAmbiguityAsync(List<string> words)
        {
            SetOutputText("Running through words... Word count is: " + words.Count + ".");
            foreach (string word in words)
            {
                if (word == words.Last())
                    await ModuleGPTInfo.DisambiguateTermsFile(word.Trim());
                if (word.Trim() != "")
                    ModuleGPTInfo.DisambiguateTermsFile(word.Trim());
            }
            if (this.FindControl<TextBox>("txtOutput") is TextBox txtOutput)
                txtOutput.Text = $"Done running! Total word count: {words.Count}. Total relationship count: {relationshipCount}. Total error count (not accepted): {errorCount}.";
        }

        public async Task ProcessParentsAsync(List<string> words)
        {
            SetOutputText("Getting Parents: Running through words... Word count is: " + words.Count + ".");
            foreach (string word in words)
            {
                if (word == words.Last())
                    await ModuleGPTInfo.GetChatGPTParents(word.Trim());
                if (word.Trim() != "")
                    ModuleGPTInfo.GetChatGPTParents(word.Trim());
            }
            SetOutputText($"Done processing unknowns! Total word count: {words.Count}. Total relationship count: {relationshipCount}. Total error count (not accepted): {errorCount}.");
        }

        static int count = 0;
        public async Task verifyAllAsync()
        {
            count = 0;
            var mf = (ModuleGPTInfo)base.ParentModule;
            SetOutputText("Verifying all is-a relationships");
            foreach (Thing t in mf.theUKS.UKSList)
            {
                if (t.Parents.FindFirst(x => x.Label == "unknownObject") != null) continue;
                if (!t.Label.StartsWith('.')) continue;
                if (t.Label == ".") continue;
                if (t == mf.theUKS.UKSList.Last())
                    await VerifyAsync(t.Label);
                else
                    VerifyAsync(t.Label);
            }
            SetOutputText($"Done verifying is-a relationships for reasonableness. Checked {count} relationships.");
        }
        public async Task VerifyAsync(string label)
        {
            var mf = (ModuleGPTInfo)base.ParentModule;
            if (!label.StartsWith(".")) label = "." + label;
            UKS.Thing t = mf.theUKS.Labeled(label);
            if (t == null) return;
            foreach (Relationship r in t.Relationships)
            {
                if (r.GPTVerified) continue;
                if (r.reltype.Label != "has-child") continue;
                count++;
                ModuleGPTInfo.GetChatGPTVerifyParentChild(r.target.Label, t.Label);
            }
        }

        private void GetGPTClausesAsync()
        {
            SetOutputText("Working...");
            ModuleGPTInfo.GetChatGPTClauses();
            SetOutputText($"\n\rTotal clause count: {relationshipCount}. Total error count (not accepted): {errorCount}.");
        }

        private void SolveAmbiguityGPTAsync()
        {
            SetOutputText("Working...");
            ModuleGPTInfo.DisambiguateTerms();
            SetOutputText($"\n\rTotal disambiguity success count: {relationshipCount}. Total error count (not accepted): {errorCount}.");
        }

        private void SolveDuplicatesAsync()
        {
            SetOutputText("Working...");
            ModuleGPTInfo.SolveDuplicates();
            SetOutputText($"\n\rDone! Duplicates resolved: {relationshipCount}.");
        }
    }
}
