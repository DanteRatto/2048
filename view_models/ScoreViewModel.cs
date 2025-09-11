using R3;

namespace ViewModels
{
    public class ScoreViewModel : ViewModel
    {
        public ReactiveProperty<int> Score { get; } = new();
        public ReactiveProperty<string> ScoreText { get; } = new();

        public ScoreViewModel(int startingScore)
        {
            Score.Value = startingScore;
            disposable = Score.Subscribe(x => ScoreText.Value = x.ToString());
        }
    }
}
