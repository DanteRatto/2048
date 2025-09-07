using Godot;
using R3;
using System;
using ViewModels;

namespace Views
{
    public partial class NumberView : View<NumberViewModel>
    {
        [Export] private Panel panel;
        [Export] private StyleBoxFlat styleBox;
        [Export] private Label label;
        [Export] private string[] texts;
        [Export] private int[] fontSizes;
        [Export] private Color[] textColors;
        [Export] private Color[] colors;

        private const string fontSizeKey = "font_size";
        private const string fontColorKey = "font_color";
        private const string panelColorKey = "panel";

        private static RandomNumberGenerator random = new();

        public override void _Ready()
        {
            base._Ready();

            var tempStyleBox = styleBox.Duplicate() as StyleBoxFlat ?? throw new ArgumentNullException(nameof(styleBox));
            panel.AddThemeStyleboxOverride(panelColorKey, tempStyleBox);
            disposable = Disposable.Combine(
                tempStyleBox,
                ViewModel = new NumberViewModel(random.RandiRange(0, 16)/* < 9 ? 0 : 1*/),
                ViewModel.Index.Subscribe(x =>
                {
                    label.Text = texts[x];
                    label.AddThemeFontSizeOverride(fontSizeKey, fontSizes[x]);
                    label.AddThemeColorOverride(fontColorKey, textColors[x]);
                    tempStyleBox.SetBgColor(colors[x]);
                }));
        }
    }
}
