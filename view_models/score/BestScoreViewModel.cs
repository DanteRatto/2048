using R3;

namespace ViewModels.Score;

public class BestScoreViewModel : ScoreViewModel
{
    public BestScoreViewModel(int startingScore, Observable<int> currentScore) : base(startingScore)
    {
        disposable = Disposable.Combine(disposable, currentScore.Subscribe(x => { if (x > Score.Value) Score.Value = x; }));
    }
}
