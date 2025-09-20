using Godot;
using R3;
using ViewModels;

namespace Views;

public partial class PopUpView : View<PopUpViewModel>
{
    [Export] private GridView gridView;
    [Export] private RestartButtonView restartButtonView;
    [Export] private Button button;
    [Export] private Label prompt;

    protected override void Initialize()
    {
        disposable = disposable = Disposable.Combine(ViewModel = new PopUpViewModel(restartButtonView.ViewModel.Restart, gridView.ViewModel.Won, gridView.ViewModel.Lost),
            ViewModel.Visible.Subscribe(SetVisible),
            ViewModel.Prompt.SubscribeToLabel(prompt),
            ViewModel.ButtonText.Subscribe(button.SetText));
        button.Pressed += ViewModel.Hide;
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        button.Pressed -= ViewModel.Hide;
    }
}
