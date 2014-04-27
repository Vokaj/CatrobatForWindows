﻿using System.Windows.Controls;
using Catrobat.IDE.Core.Resources.Localization;

namespace Catrobat.IDE.Phone.Controls.FormulaControls.PartControls
{
    public class FormulaPartControlStaticLocalizedText : FormulaPartControl
    {
        public string LocalizationKey { get; set; }

        protected override Grid CreateControls(double fontSize, bool isParentSelected, bool isSelected, bool isError)
        {
            var textBlock = new TextBlock
            {
                Text = Text,
                FontSize = fontSize
            };

            var grid = new Grid { DataContext = this };
            grid.Children.Add(textBlock);

            return grid;
        }

        public string Text
        {
            get { return AppResources.ResourceManager.GetString(LocalizationKey); }
        }

        public override int GetCharacterWidth()
        {
            return Text.Length;
        }

        public override FormulaPartControl Copy()
        {
            return new FormulaPartControlStaticLocalizedText
            {
                Style = Style,
                LocalizationKey = this.LocalizationKey
            };
        }
    }
}
