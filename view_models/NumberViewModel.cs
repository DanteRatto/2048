using R3;

namespace ViewModels
{
    public class NumberViewModel : ViewModel
    {
        public ReactiveProperty<int> Index { get; } = new();
        public ReactiveProperty<int> X { get; } = new();
        public ReactiveProperty<int> Y { get; } = new();

        public NumberViewModel(int index)
        {
            Index.Value = index;
        }
    }
}
