using R3;

namespace ViewModels;

public class MainViewModel : ViewModel
{
    private readonly GridViewModel grid;
    private readonly ScoreViewModel current;
    private readonly PopUpViewModel popUp;

    public MainViewModel(GridViewModel grid, ScoreViewModel current, ScoreViewModel best, PopUpViewModel popUp)
    {
        this.grid = grid;
        this.current = current;
        this.popUp = popUp;
        disposable = Disposable.Combine(current.Score.Subscribe(x => { if (x > best.Score.Value) best.Score.Value = x; }),
            grid.Won.Subscribe(x =>
            {
                if (x)
                {
                    popUp.Prompt.Value = "You Win!";
                    popUp.ButtonText.Value = "Continue";
                    popUp.Visible.Value = true;
                }
            }),
            grid.Lost.Subscribe(x =>
            {
                if (x)
                {
                    popUp.Prompt.Value = "Game Over";
                    popUp.ButtonText.Value = "Play Again";
                    popUp.Visible.Value = true;
                }
            }));
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

    public void PopUpButtonPress()
    {
        if (!grid.Lost.Value) return;
        Restart();
    }
}
