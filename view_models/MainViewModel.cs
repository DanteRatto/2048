using R3;

namespace ViewModels;

public class MainViewModel : ViewModel
{
    private readonly GridViewModel grid;
    private readonly ScoreViewModel current;

    public MainViewModel(GridViewModel grid, ScoreViewModel current, ScoreViewModel best)
    {
        this.grid = grid;
        this.current = current;
        disposable = current.Score.Subscribe(x => { if (x > best.Score.Value) best.Score.Value = x; });
        foreach (var number in grid.Numbers)
        {
            // Skip(1) to ignore initial index
            disposable = Disposable.Combine(disposable, number.Index.Skip(1).Subscribe(x => { if (number.Visible.Value) current.Score.Value += number.Value; }));
        }
    }

    public void Restart()
    {
        grid.Reset();
        current.Score.Value = 0;
    }
}
