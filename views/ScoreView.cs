using Godot;
using R3;
using System.Collections.Generic;
using ViewModels;

namespace Views;

public partial class ScoreView : View<ScoreViewModel>
{
    [Export] private Label[] labels;
    [Export] private string key;

    private const string saveFile = "user://scores.cfg";
    private const string section = "score";

    private static readonly ConfigFile configFile = new();
    private static readonly IReadOnlyList<string> numberTexts = ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9"];

    public override void _Ready()
    {
        base._Ready();
        disposable = Disposable.Combine(
            ViewModel = new ScoreViewModel(configFile.Load(saveFile) == Error.Ok && configFile.HasSectionKey(section, key) ? (int)configFile.GetValue(section, key) : 0),
            ViewModel.Score.Subscribe(x =>
            {
                SetScoreText(x);
                configFile.SetValue(section, key, x);
            }));
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        disposable?.Dispose();
        configFile.Save(saveFile);
    }

    public override void _Notification(int what)
    {
        if (what == NotificationApplicationFocusOut || what == NotificationWMWindowFocusOut || what == NotificationWMCloseRequest) configFile.Save(saveFile);
        if (what == NotificationWMCloseRequest) GetTree().Quit();
        base._Notification(what);
    }

    private void SetScoreText(int score)
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
