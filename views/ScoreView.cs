using Godot;
using R3;
using ViewModels;

namespace Views
{
    public partial class ScoreView : View<ScoreViewModel>
    {
        [Export] private Label label;
        [Export] private string key;

        private const string saveFile = "user://scores.cfg";

        private static readonly ConfigFile configFile = new();

        public override void _Ready()
        {
            base._Ready();
            disposable = Disposable.Combine(
                ViewModel = new ScoreViewModel(configFile.Load(saveFile) == Error.Ok && configFile.HasSectionKey("score", key) ? (int)configFile.GetValue("score", key) : 0),
                ViewModel.ScoreText.SubscribeToLabel(label),
                ViewModel.Score.Subscribe(x => configFile.SetValue("score", key, x)));
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
    }
}
