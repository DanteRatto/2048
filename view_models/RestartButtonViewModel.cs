using System;

namespace ViewModels;

public class RestartButtonViewModel : ViewModel
{
    private readonly Action resetGrid;
    private readonly Action resetScore;

    public RestartButtonViewModel(Action resetGrid, Action resetScore)
    {
        this.resetGrid = resetGrid;
        this.resetScore = resetScore;
    }

    public void Restart()
    {
        resetGrid.Invoke();
        resetScore.Invoke();
    }
}
