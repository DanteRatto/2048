using System;
using System.Collections.Generic;

namespace ViewModels
{
    public class GridViewModel : ViewModel
    {
        private readonly NumberViewModel[,] grid = new NumberViewModel[4, 4];
        private readonly Stack<NumberViewModel> inactiveNumbers = new();
        private readonly Func<int, int> GetRandom;

        public GridViewModel(IEnumerable<NumberViewModel> numbers, Func<int, int> getRandom)
        {
            GetRandom = getRandom;
            foreach (var number in numbers) inactiveNumbers.Push(number);
            for (var i = 0; i < 2; ++i) AddNumber(); // add 2 numbers to start
        }

        private void AddNumber()
        {
            for (int x = 0, skip = GetRandom(inactiveNumbers.Count - 1); x < grid.GetLength(0); ++x) // get the total available grid slots and pick a random one
            {
                for (var y = 0; y < grid.GetLength(1); ++y)
                {
                    if (grid[x, y] == null && skip-- > 0) continue;
                    var number = inactiveNumbers.Pop();
                    number.SetIndex();
                    grid[x, y] = number;
                    number.X.Value = x;
                    number.Y.Value = y;
                    return;
                }
            }
        }

        private void RemoveNumber(NumberViewModel number)
        {
            inactiveNumbers.Push(number);
            grid[number.X.Value, number.Y.Value] = null;
            number.X.Value = -1;
        }

        private void RemoveNumber(int x, int y) => RemoveNumber(grid[x, y]);
    }
}
