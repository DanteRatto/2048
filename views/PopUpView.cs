using Godot;
using R3;
using ViewModels;

namespace Views;

public partial class PopUpView : View<PopUpViewModel>
{
    [Export] private Label prompt;
    [Export] private Button button;

    public override void _Ready() // wait a frame so view models can be set
    {
        base._Ready();
        disposable = disposable = Disposable.Combine(ViewModel = new PopUpViewModel(),
            ViewModel.Visible.Subscribe(SetVisible),
            ViewModel.Prompt.SubscribeToLabel(prompt),
            ViewModel.ButtonText.Subscribe(button.SetText));
    }
}
