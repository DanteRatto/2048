using Godot;
using R3;
using ViewModels;

namespace Views;

public partial class GridView : View<GridViewModel>
{
    [Export] private PackedScene number;
    [Export] private GridContainer gridContainer;
    [Export] private Control numbersContainer;

    private static RandomNumberGenerator random = new();

    private readonly Vector2[,] positions = new Vector2[4, 4];

    protected override void Initialize()
    {
        var numbers = new NumberViewModel[positions.Length];
        for (var i = 0; i < numbers.Length; ++i)
        {
            var numberView = number.Instantiate<NumberView>();
            numbersContainer.AddChild(numberView);
            numbers[i] = numberView.ViewModel;
            var activeSub = numberView.ViewModel.Visible.Subscribe(numberView.SetVisible);
            var xSub = numberView.ViewModel.X.Subscribe(x => numberView.Position = positions[x, numberView.ViewModel.Y.Value]);
            var ySub = numberView.ViewModel.Y.Subscribe(y => numberView.Position = positions[numberView.ViewModel.X.Value, y]);
            disposable = disposable != null ? Disposable.Combine(disposable, activeSub, xSub, ySub) : Disposable.Combine(activeSub, xSub, ySub);
        }
        disposable = Disposable.Combine(disposable, ViewModel = new GridViewModel(numbers, x => random.RandiRange(0, x)));
    }

    protected override void _LateReady() // wait a frame so GridContainer can set positions of the tiles
    {
        base._LateReady();
        for (int y = 0, i = 0; y < positions.GetLength(0); ++y)
        {
            for (var x = 0; x < positions.GetLength(1); ++x, ++i)
            {
                positions[x, y] = gridContainer.GetChild<Control>(i).Position;
            }
        }
        ViewModel.Start();
    }

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
        if (@event.IsActionPressed("ui_up")) ViewModel.Up();
        else if (@event.IsActionPressed("ui_left")) ViewModel.Left();
        else if (@event.IsActionPressed("ui_down")) ViewModel.Down();
        else if (@event.IsActionPressed("ui_right")) ViewModel.Right();
    }
}
