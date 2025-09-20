using System;
using ViewModels.Score;

namespace ViewModels;

public class RestartButtonViewModel : ViewModel
{
    private readonly Action ResetGrid;
    private readonly ScoreViewModel current;

    public RestartButtonViewModel(Action resetGrid, ScoreViewModel current)
    {
        ResetGrid = resetGrid;
        this.current = current;
    }

    public void Restart()
    {
        ResetGrid.Invoke();
        current.Score.Value = 0;
    }
}
