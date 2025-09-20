using Godot;
using System.Collections.Generic;
using Utilities;
using ViewModels.Score;

namespace Views.Score;

public abstract partial class ScoreView : View<ScoreViewModel>
{
    [Export] private SaveManager saveManager;
    [Export] private Label[] labels;

    protected abstract string Key { get; }

    private const string section = "score";

    private static readonly IReadOnlyList<string> numberTexts = ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9"];

    public override void _Notification(int what)
    {
        if (what == NotificationApplicationFocusOut || what == NotificationWMWindowFocusOut || what == NotificationWMCloseRequest)
        {
            saveManager.ConfigFile.SetValue(section, Key, ViewModel.Score.Value);
        }
        base._Notification(what);
    }

    protected int LoadScore() => saveManager.ConfigFile.LoadInt(section, Key);

    protected void SetScoreText(int score)
    {
        for (var i = 0; i < labels.Length; ++i)
        {
            var digit = score % 10;
            labels[i].SetText(numberTexts[digit]);
            labels[i].SetVisible(score > 0 || i == 0);
            score /= 10;
        }
    }
}
