using Godot;
using ViewModels;
using Views.Score;

namespace Views;

public partial class RestartButtonView : View<RestartButtonViewModel>
{
    [Export] private GridView gridView;
    [Export] private ScoreView scoreView;
    [Export] private Button button;

    protected override void Initialize()
    {
        disposable = ViewModel = new RestartButtonViewModel(gridView.ViewModel.Reset, scoreView.ViewModel.Reset);
        button.Pressed += ViewModel.Restart;
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        button.Pressed -= ViewModel.Restart;
    }
}
