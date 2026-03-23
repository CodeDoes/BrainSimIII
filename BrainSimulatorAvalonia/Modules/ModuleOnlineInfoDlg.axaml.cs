// Copyright (c) FutureAI. All rights reserved.
// Contains confidential and proprietary information and programs which may not be distributed without a separate license
using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Input;

namespace BrainSimulatorAvalonia.Modules
{
    public partial class ModuleOnlineInfoDlg : ModuleBaseDlg
    {
        string prevOutput;
        public ModuleOnlineInfoDlg()
        {
            InitializeComponent();
        }

        public override bool Draw(bool checkDrawTimer)
        {
            if (!base.Draw(checkDrawTimer)) return false;
            var mcn = (ModuleOnlineInfo)base.ParentModule;
            if (this.FindControl<TextBox>("txtOutput") is TextBox txtOutput)
            {
                if (prevOutput != mcn.Output)
                {
                    txtOutput.Text = mcn.Output;
                    prevOutput = mcn.Output;
                }
            }
            return true;
        }

        private void txtInput_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && this.FindControl<TextBox>("txtInput") is TextBox txtInput)
            {
                string txt = txtInput.Text;
                var mcn = (ModuleOnlineInfo)base.ParentModule;
                var relationshipSelection = this.FindControl<ComboBox>("relationshipSelection");
                var comboSelection = this.FindControl<ComboBox>("comboSelection");
                string currentRelationship = ((ComboBoxItem)relationshipSelection.SelectedItem).Content.ToString();
                ModuleOnlineInfo.QueryType qType = ModuleOnlineInfo.QueryType.isa;
                string currentSearch = ((ComboBoxItem)comboSelection.SelectedItem).Content.ToString();
                switch (currentRelationship)
                {
                    case "is-a":
                        qType = ModuleOnlineInfo.QueryType.isa;
                        break;
                    case "hasa":
                        qType = ModuleOnlineInfo.QueryType.hasa;
                        break;
                }
                switch (currentSearch)
                {
                    case "ChatGPT":
                        if (txt.EndsWith("?"))
                            mcn.GetChatGPTResult(txt, ModuleOnlineInfo.QueryType.general);
                        else if (txt.StartsWith("list some"))
                            mcn.GetChatGPTResult(txt.Substring(10), ModuleOnlineInfo.QueryType.list);
                        else if (txt.StartsWith("count some"))
                            mcn.GetChatGPTResult(txt.Substring(11), ModuleOnlineInfo.QueryType.listCount);
                        else if (txt.EndsWith("can"))
                            mcn.GetChatGPTResult(txt.Substring(0, txt.Length - 3), ModuleOnlineInfo.QueryType.can);
                        else
                        {
                            mcn.GetChatGPTResult(txt, qType);
                        }
                        break;
                    case "ConceptNet":
                        mcn.GetConceptNetData(txt);
                        break;
                    case "WikiData":
                        mcn.GetWikidataData(txt, "subclass of");
                        break;
                    case "Wiktionary":
                        mcn.GetWiktionaryData(txt);
                        break;
                    case "Free Dictionary":
                        mcn.GetFreeDictionaryAPIData(txt);
                        break;
                    case "Webster's Elementary":
                        mcn.GetWebstersDictionaryAPIData(txt);
                        break;
                    case "Kid's Definition":
                        mcn.GetKidsDefinition(txt);
                        break;
                    case "CSKG":
                        mcn.GetCSKGData(txt);
                        break;
                    case "Oxford Word List":
                        mcn.SetupWordList2(txt);
                        break;
                }
            }
        }

        private void comboSelection_SelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            // Optionally handle selection change if needed
        }

        private void ClearButton_Click(object? sender, RoutedEventArgs e)
        {
            if (this.FindControl<TextBox>("txtOutput") is TextBox txtOutput)
                txtOutput.Text = "";
        }
    }
}
