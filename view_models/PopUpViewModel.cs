using R3;
using System;

namespace ViewModels;

public class PopUpViewModel : ViewModel
{
    public ReactiveProperty<bool> Visible { get; } = new();
    public ReactiveProperty<string> Prompt { get; } = new();
    public ReactiveProperty<string> ButtonText { get; } = new();

    private ReadOnlyReactiveProperty<bool> Lost { get; }

    private readonly Action Restart;

    public PopUpViewModel(Action restart, Observable<bool> won, Observable<bool> lost)
    {
        Restart = restart;
        disposable = Disposable.Combine(Lost = lost.ToReadOnlyReactiveProperty(),
            won.Subscribe(x =>
            {
                if (x)
                {
                    Prompt.Value = "You Win!";
                    ButtonText.Value = "Continue";
                    Visible.Value = true;
                }
            }),
            lost.Subscribe(x =>
            {
                if (x)
                {
                    Prompt.Value = "Game Over";
                    ButtonText.Value = "Play Again";
                    Visible.Value = true;
                }
            }));
    }

    public void Hide()
    {
        if (!Lost.CurrentValue) return;
        Restart.Invoke();
        Visible.Value = false;
    }
}
