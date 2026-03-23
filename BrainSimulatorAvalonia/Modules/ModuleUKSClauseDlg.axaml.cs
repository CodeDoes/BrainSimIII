using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System;
using System.Collections.Generic;
using UKS;

namespace BrainSimulatorAvalonia.Modules
{
    public partial class ModuleUKSClauseDlg : UserControl
    {
        private ComboBox? SourceDisambiguation;
        private TextBox? sourceText;
        private TextBox? relationshipText;
        private TextBox? targetText;
        private TextBox? clauseTypeText;
        private TextBox? sourceText2;
        private TextBox? relationshipText2;
        private TextBox? targetText2;
        private Button? BtnAddRelationship;
        private Relationship? rBase = null;

        public ModuleUKSClauseDlg()
        {
            AvaloniaXamlLoader.Load(this);
            sourceText = this.FindControl<TextBox>("sourceText");
            relationshipText = this.FindControl<TextBox>("relationshipText");
            targetText = this.FindControl<TextBox>("targetText");
            clauseTypeText = this.FindControl<TextBox>("clauseTypeText");
            sourceText2 = this.FindControl<TextBox>("sourceText2");
            relationshipText2 = this.FindControl<TextBox>("relationshipText2");
            targetText2 = this.FindControl<TextBox>("targetText2");
            SourceDisambiguation = this.FindControl<ComboBox>("SourceDisambiguation");
            BtnAddRelationship = this.FindControl<Button>("BtnAddRelationship");

            if (BtnAddRelationship != null)
                BtnAddRelationship.Click += BtnAddRelationship_Click;
            if (sourceText != null)
                sourceText.PropertyChanged += Text_TextChanged;
            if (relationshipText != null)
                relationshipText.PropertyChanged += Text_TextChanged;
            if (targetText != null)
                targetText.PropertyChanged += Text_TextChanged;
            if (clauseTypeText != null)
                clauseTypeText.PropertyChanged += Text_TextChanged;
            if (sourceText2 != null)
                sourceText2.PropertyChanged += Text_TextChanged;
            if (relationshipText2 != null)
                relationshipText2.PropertyChanged += Text_TextChanged;
            if (targetText2 != null)
                targetText2.PropertyChanged += Text_TextChanged;
            if (SourceDisambiguation != null)
                SourceDisambiguation.SelectionChanged += SourceDisambiguation_SelectionChanged;
        }

        private void BtnAddRelationship_Click(object? sender, RoutedEventArgs e)
        {
            if (sourceText == null || targetText == null || relationshipText == null || clauseTypeText == null || sourceText2 == null || relationshipText2 == null || targetText2 == null)
                return;

            string newThing = sourceText.Text;
            string targetThing = targetText.Text;
            string relationType = relationshipText.Text;
            string clauseLabel = clauseTypeText.Text.ToUpper();

            if (!CheckAddRelationshipFieldsFilled()) return;

            if (Parent is not Window parentWindow || parentWindow.DataContext is not ModuleUKSClause UKSClause)
                return;

            Thing source = UKSClause.theUKS.CreateThingFromMultipleAttributes(newThing, false);
            Thing target = UKSClause.theUKS.CreateThingFromMultipleAttributes(targetThing, false);
            Thing relType = UKSClause.theUKS.CreateThingFromMultipleAttributes(relationType, true);
            Thing clauseType = UKSClause.theUKS.CreateThingFromMultipleAttributes(clauseLabel, false);

            Relationship? r1 = null;
            if (rBase != null)
            {
                if (GetInstanceRoot(rBase.source) != source ||
                    GetInstanceRoot(rBase.target) != target ||
                    GetInstanceRoot(rBase.relType) != relType)
                    rBase = null;
                if (rBase != null && !rBase.source.Relationships.Contains(rBase))
                    rBase = null;
                if (rBase != null)
                    r1 = rBase;
            }
            if (r1 == null)
                r1 = UKSClause.theUKS.AddStatement(source, relType, target, false);

            Thing theClauseType = UKSClause.theUKS.GetOrAddThing(clauseLabel, "ClauseType");
            Thing source2 = UKSClause.theUKS.CreateThingFromMultipleAttributes(sourceText2.Text, false);
            Thing type2 = UKSClause.theUKS.CreateThingFromMultipleAttributes(relationshipText2.Text, true);
            Thing target2 = UKSClause.theUKS.CreateThingFromMultipleAttributes(targetText2.Text, false);
            Relationship rAdded = UKSClause.theUKS.AddClause(r1, theClauseType, source2, type2, target2);

            SetUpRelComboBox(GetInstanceRoot(r1.source), rAdded);
        }

        private Thing GetInstanceRoot(Thing t)
        {
            Thing t1 = t;
            while (t1.HasProperty("isInstance")) t1 = t1.Parents[0];
            return t1;
        }

        private void Text_TextChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
        {
            // Only respond to Text property changes
            if (e.Property.Name != "Text") return;
            if (sender is TextBox tb && tb.Name == "sourceText")
            {
                Thing? sourceThing = CheckThingExistence(tb);
                SetUpRelComboBox(sourceThing);
            }
        }

        private void SetUpRelComboBox(Thing? sourceThing, Relationship? rSelected = null)
        {
            if (SourceDisambiguation == null) return;
            SourceDisambiguation.Items = new List<string> { "<new>" };
            SourceDisambiguation.SelectedIndex = 0;
            rBase = null;
            if (sourceThing != null)
            {
                var items = new List<string> { "<new>" };
                foreach (Thing t in sourceThing.Descendents)
                {
                    if (t != sourceThing && !t.HasProperty("isInstance")) continue;
                    foreach (Relationship r in t.Relationships)
                    {
                        if (r.reltype.Label == "has-child" || r.reltype.Label == "hasProperty") continue;
                        items.Add(r.target.Label);
                        if (r == rSelected)
                            SourceDisambiguation.SelectedItem = r.target.Label;
                    }
                }
                SourceDisambiguation.Items = items;
                SourceDisambiguation.IsVisible = items.Count > 1;
            }
            else
            {
                SourceDisambiguation.IsVisible = false;
            }
        }

        private void SourceDisambiguation_SelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            // TODO: Implement logic to update relationshipText and targetText based on selection
        }

        private Thing? CheckThingExistence(TextBox tb)
        {
            string text = tb.Text.Trim();
            if (string.IsNullOrEmpty(text) && !tb.Name.Contains("arget"))
            {
                // TODO: Set background color to indicate error
                SetStatus("Source and type cannot be empty");
                return null;
            }
            // TODO: Implement ThingListFromString logic or call equivalent
            // For now, just return null
            return null;
        }

        private bool CheckAddRelationshipFieldsFilled()
        {
            SetStatus("");
            if (sourceText == null || relationshipText == null || clauseTypeText == null || sourceText2 == null || relationshipText2 == null)
                return false;
            if (string.IsNullOrEmpty(sourceText.Text)) { SetStatus("Source not provided"); return false; }
            if (string.IsNullOrEmpty(relationshipText.Text)) { SetStatus("Type not provided"); return false; }
            if (string.IsNullOrEmpty(clauseTypeText.Text)) { SetStatus("Clause type not provided"); return false; }
            if (string.IsNullOrEmpty(sourceText2.Text)) { SetStatus("Clause source not provided"); return false; }
            if (string.IsNullOrEmpty(relationshipText2.Text)) { SetStatus("Clause type not provided"); return false; }
            return true;
        }

        private void SetStatus(string message)
        {
            // TODO: Implement status display in Avalonia dialog
        }
    }
}
