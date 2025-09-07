using Godot;
using R3;
using System.Linq;
using ViewModels;

namespace Views
{
    public partial class GridView : View<GridViewModel>
    {
        [Export] private PackedScene number;
        [Export] private Control active;
        [Export] private Control inactive;

        private static RandomNumberGenerator random = new();

        public override void _Ready()
        {
            base._Ready();
            var numbers = new NumberView[16];
            for (var i = 0; i < numbers.Length; ++i)
            {
                var numberViewModel = new NumberViewModel(() => random.RandiRange(0, 9) < 9 ? 0 : 1);
                var numberView = number.Instantiate<NumberView>();
                inactive.AddChild(numberView);
                numberView.SetViewModel(numberViewModel);
                numbers[i] = numberView;
                var sub = numberViewModel.X.Subscribe(x => numberView.Reparent(x < 0 ? inactive : active, false));
                disposable = disposable != null ? Disposable.Combine(disposable, sub) : sub;
            }
            disposable = Disposable.Combine(disposable, ViewModel = new GridViewModel(numbers.Select(x => x.ViewModel), x => random.RandiRange(0, x)));
        }
    }
}
