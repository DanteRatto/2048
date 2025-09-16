using Godot;
using R3;
using ViewModels;

namespace Views
{
    public partial class GridView : View<GridViewModel>
    {
        [Export] private PackedScene number;
        [Export] private GridContainer gridContainer;
        [Export] private Control numbersContainer;

        private static RandomNumberGenerator random = new();

        protected override void _LateReady() // wait a frame so GridContainer can set positions of the tiles
        {
            base._LateReady();
            var grid = new Vector2[4, 4];
            for (int y = 0, i = 0; y < grid.GetLength(0); ++y)
            {
                for (var x = 0; x < grid.GetLength(1); ++x, ++i)
                {
                    grid[x, y] = gridContainer.GetChild<Control>(i).Position;
                }
            }
            var numbers = new NumberViewModel[grid.Length];
            for (var i = 0; i < numbers.Length; ++i)
            {
                var numberViewModel = new NumberViewModel(() => random.RandiRange(0, 9) < 9 ? 0 : 1);
                var numberView = number.Instantiate<NumberView>();
                numbersContainer.AddChild(numberView);
                numberView.SetViewModel(numberViewModel);
                numbers[i] = numberView.ViewModel;
                var activeSub = numberViewModel.Visible.Subscribe(numberView.SetVisible);
                var xSub = numberViewModel.X.Subscribe(x => numberView.Position = grid[x, numberViewModel.Y.Value]);
                var ySub = numberViewModel.Y.Subscribe(y => numberView.Position = grid[numberViewModel.X.Value, y]);
                disposable = disposable != null ? Disposable.Combine(disposable, activeSub, xSub, ySub) : Disposable.Combine(activeSub, xSub, ySub);
            }
            disposable = Disposable.Combine(disposable, ViewModel = new GridViewModel(numbers, x => random.RandiRange(0, x)));
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
}
