using Godot;
using R3;
using ViewModels;

namespace Views;

public partial class PopUpView : View<PopUpViewModel>
{
    [Export] public Button Button { get; private set; }
    [Export] private Label prompt;

    public override void _Ready() // wait a frame so view models can be set
    {
        base._Ready();
        disposable = disposable = Disposable.Combine(ViewModel = new PopUpViewModel(),
            ViewModel.Visible.Subscribe(SetVisible),
            ViewModel.Prompt.SubscribeToLabel(prompt),
            ViewModel.ButtonText.Subscribe(Button.SetText));
        Button.Pressed += ViewModel.Hide;
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        Button.Pressed -= ViewModel.Hide;
    }
}
