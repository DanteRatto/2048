using Godot;
using R3;
using ViewModels;

namespace Views;

public partial class MainView : View<MainViewModel>
{
    [Export] private ScoreView scoreView;
    [Export] private ScoreView bestScoreView;
    [Export] private GridView gridView;
    [Export] private Button restartButton;

    protected override void _LateReady() // wait a frame so view models can be set
    {
        base._LateReady();
        disposable = Disposable.Combine(ViewModel = new MainViewModel(gridView.ViewModel, scoreView.ViewModel, bestScoreView.ViewModel),
            gridView.ViewModel.Lost.Subscribe(x => { if (x) GD.Print("You Lose!"); }),
            gridView.ViewModel.Won.Subscribe(x => { if (x) GD.Print("You Win!"); }));
        restartButton.Pressed += ViewModel.Restart;
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        restartButton.Pressed -= ViewModel.Restart;
    }
}
