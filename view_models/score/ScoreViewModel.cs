using R3;

namespace ViewModels.Score;

public abstract class ScoreViewModel : ViewModel
{
    public ReactiveProperty<int> Score { get; } = new();

    protected ScoreViewModel(int startingScore)
    {
        Score.Value = startingScore;
    }
}
