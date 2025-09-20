using Godot;
using System.Collections.Generic;
using Utilities;
using ViewModels.Score;

namespace Views.Score;

public abstract partial class ScoreView : View<ScoreViewModel>
{
    [Export] private Label[] labels;

    protected abstract string Key { get; }

    private const string saveFile = "user://save.cfg";
    private const string section = "score";

    private static readonly ConfigFile configFile = new();
    private static readonly IReadOnlyList<string> numberTexts = ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9"];

    public override void _ExitTree()
    {
        base._ExitTree();
        configFile.SetAndSave(saveFile, section, Key, ViewModel.Score.Value);
        disposable?.Dispose();
    }

    public override void _Notification(int what)
    {
        if (what == NotificationApplicationFocusOut || what == NotificationWMWindowFocusOut || what == NotificationWMCloseRequest) configFile.Save(saveFile);
        if (what == NotificationWMCloseRequest) GetTree().Quit();
        base._Notification(what);
    }

    protected int LoadScore() => configFile.LoadInt(saveFile, section, Key);

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
