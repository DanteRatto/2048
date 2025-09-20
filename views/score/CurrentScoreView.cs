using Godot;
using R3;
using ViewModels.Score;

namespace Views.Score;

public partial class CurrentScoreView : ScoreView
{
    [Export] private GridView grid;

    protected override string Key { get; } = "current";

    protected override void Initialize()
    {
        disposable = Disposable.Combine(ViewModel = new CurrentScoreViewModel(LoadScore(), grid.ViewModel.Numbers),
            ViewModel.Score.Subscribe(SetScoreText));
    }
}
