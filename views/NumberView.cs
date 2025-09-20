using Godot;
using R3;
using System;
using Utilities;
using ViewModels;

namespace Views;

public partial class NumberView : View<NumberViewModel>
{
    [Export] private Panel panel;
    [Export] private StyleBoxFlat styleBox;
    [Export] private Label label;

    private static readonly RandomNumberGenerator random = new();

    protected override void Initialize()
    {
        var tempStyleBox = styleBox.Duplicate() as StyleBoxFlat ?? throw new ArgumentNullException(nameof(styleBox));
        panel.AddThemeStyleboxOverride("panel", tempStyleBox);
        disposable = Disposable.Combine(
            tempStyleBox,
            ViewModel = new NumberViewModel(() => random.RandiRange(0, 9) < 9 ? 0 : 1),
            ViewModel.Text.SubscribeToLabel(label),
            ViewModel.FontSize.Subscribe(x => label.AddThemeFontSizeOverride("font_size", x)),
            ViewModel.BackgroundColor.Subscribe(x => tempStyleBox.SetBgColor(x.ToColor())),
            ViewModel.TextColor.Subscribe(x => label.AddThemeColorOverride("font_color", x.ToColor())));
    }
}
