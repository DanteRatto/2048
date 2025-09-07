using R3;

namespace ViewModels
{
    public class ScoreViewModel : ViewModel
    {
        public ReactiveProperty<uint> Score { get; } = new();
        public ReactiveProperty<string> ScoreText { get; } = new("0");

        public ScoreViewModel(uint startingScore)
        {
            Score.Value = startingScore;
            disposable = Score.Subscribe(x => ScoreText.Value = x.ToString());
        }
    }
}
