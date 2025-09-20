using Godot;
using R3;
using ViewModels.Score;

namespace Views.Score;

public partial class BestScoreView : ScoreView
{
    [Export] private ScoreView current;

    protected override string Key { get; } = "best";

    protected override void Initialize()
    {
        disposable = Disposable.Combine(ViewModel = new BestScoreViewModel(LoadScore(), current.ViewModel.Score),
            ViewModel.Score.Subscribe(SetScoreText));
    }
}
