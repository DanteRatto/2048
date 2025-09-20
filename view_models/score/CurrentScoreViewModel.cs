using R3;
using System.Collections.Generic;

namespace ViewModels.Score;

public class CurrentScoreViewModel : ScoreViewModel
{
    public CurrentScoreViewModel(int startingScore, IReadOnlyList<NumberViewModel> numbers) : base(startingScore)
    {
        foreach (var number in numbers)
        {
            // Skip(1) to ignore initial index
            disposable = Disposable.Combine(disposable, number.Index.Skip(1).Subscribe(x => { if (number.Visible.Value) Score.Value += number.Value; }));
        }
    }
}
