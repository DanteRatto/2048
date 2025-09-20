using R3;

namespace ViewModels;

public class PopUpViewModel : ViewModel
{
    public ReactiveProperty<bool> Visible { get; } = new();
    public ReactiveProperty<string> Prompt { get; } = new();
    public ReactiveProperty<string> ButtonText { get; } = new();

    public void Hide()
    {
        Visible.Value = false;
    }
}
