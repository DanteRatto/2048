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

        private static readonly ConfigFile config = new();

        public override void _Ready()
        {
            base._Ready();
            disposable = Disposable.Combine(
                ViewModel = new ScoreViewModel(config.Load(saveFile) != Error.Ok && config.HasSectionKey("score", key) ? (uint)config.GetValue("score", key) : 0),
                ViewModel.ScoreText.SubscribeToLabel(label));
        }

        public override void _ExitTree()
        {
            base._ExitTree();
            disposable?.Dispose();
            config.Save(saveFile);
        }
    }
}
