using Godot;
using ViewModels;

namespace Views
{
    public partial class MainView : View<MainViewModel>
    {
        [Export] private ScoreView scoreView;
        [Export] private ScoreView bestScoreView;
        [Export] private GridView gridView;
        [Export] private Button restartButton;

        protected override void _LateReady() // wait a frame so view models can be set
        {
            base._LateReady();
            disposable = ViewModel = new MainViewModel(gridView.ViewModel, scoreView.ViewModel, bestScoreView.ViewModel);
            restartButton.Pressed += ViewModel.Restart;
        }

        public override void _ExitTree()
        {
            base._ExitTree();
            restartButton.Pressed -= ViewModel.Restart;
        }
    }
}
