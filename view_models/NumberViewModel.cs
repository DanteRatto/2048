using R3;
using System;
using System.Collections.Generic;

namespace ViewModels
{
    public class NumberViewModel : ViewModel
    {
        private struct Number
        {
            public int Value { get; set; }
            public string Text { get; set; }
            public int FontSize { get; set; }
            public (int r, int g, int b) BackgroundColor { get; set; }
            public (int r, int g, int b) TextColor { get; set; }
        }

        private static readonly IReadOnlyList<Number> numbers =
        [
            new Number{ Value = 2, Text = "2", FontSize = 80, BackgroundColor = (238, 228, 218),  TextColor = (187, 173, 160) },
            new Number{ Value = 4, Text = "4", FontSize = 80, BackgroundColor = (237, 224, 200),  TextColor = (187, 173, 160) },
            new Number{ Value = 8, Text = "8", FontSize = 80, BackgroundColor = (242, 177, 121),  TextColor = (255, 255, 255) },
            new Number{ Value = 16, Text = "16", FontSize = 80, BackgroundColor = (245, 149, 99),  TextColor = (255, 255, 255) },
            new Number{ Value = 32, Text = "32", FontSize = 80, BackgroundColor = (246, 124, 95),  TextColor = (255, 255, 255) },
            new Number{ Value = 64, Text = "64", FontSize = 80, BackgroundColor = (246, 94, 59),  TextColor = (255, 255, 255) },
            new Number{ Value = 128, Text = "128", FontSize = 72, BackgroundColor = (237, 207, 114),  TextColor = (255, 255, 255) },
            new Number{ Value = 256, Text = "256", FontSize = 72, BackgroundColor = (237, 204, 97),  TextColor = (255, 255, 255) },
            new Number{ Value = 512, Text = "512", FontSize = 72, BackgroundColor = (237, 200, 80),  TextColor = (255, 255, 255) },
            new Number{ Value = 1024, Text = "1024", FontSize = 58, BackgroundColor = (237, 197, 63),  TextColor = (255, 255, 255) },
            new Number{ Value = 2048, Text = "2048", FontSize = 58, BackgroundColor = (237, 194, 50),  TextColor = (255, 255, 255) },
            new Number{ Value = 4096, Text = "4096", FontSize = 58, BackgroundColor = (60, 58, 50),  TextColor = (255, 255, 255) },
            new Number{ Value = 8192, Text = "8192", FontSize = 58, BackgroundColor = (50, 48, 40),  TextColor = (255, 255, 255) },
            new Number{ Value = 16384, Text = "16384", FontSize = 47, BackgroundColor = (40, 38, 30),  TextColor = (255, 255, 255) },
            new Number{ Value = 32768, Text = "32768", FontSize = 47, BackgroundColor = (30, 28, 20),  TextColor = (255, 255, 255) },
            new Number{ Value = 65536, Text = "65536", FontSize = 47, BackgroundColor = (20, 18, 10),  TextColor = (255, 255, 255) },
            new Number{ Value = 131072, Text = "131072", FontSize = 39, BackgroundColor = (10, 8, 0),  TextColor = (255, 255, 255) }
        ];

        public ReactiveProperty<int> X { get; } = new(-1); // -1 is off the board
        public ReactiveProperty<int> Y { get; } = new();
        public ReactiveProperty<string> Text { get; } = new();
        public ReactiveProperty<int> FontSize { get; } = new();
        public ReactiveProperty<(int r, int g, int b)> BackgroundColor { get; } = new();
        public ReactiveProperty<(int r, int g, int b)> TextColor { get; } = new();
        private ReactiveProperty<int> Index { get; } = new();

        public int Value { get; private set; }

        private readonly Func<int> GetRandomIndex;

        public NumberViewModel(Func<int> getRandomIndex)
        {
            GetRandomIndex = getRandomIndex;
            disposable = Index.Subscribe(x =>
            {
                var number = numbers[x];
                Value = number.Value;
                Text.Value = number.Text;
                FontSize.Value = number.FontSize;
                BackgroundColor.Value = number.BackgroundColor;
                TextColor.Value = number.TextColor;
            });
        }

        public void SetIndex() => Index.Value = GetRandomIndex();
    }
}
