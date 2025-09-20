using R3;
using System;
using System.Collections.Generic;
using ZLinq;

namespace ViewModels;

public class GridViewModel : ViewModel
{
    public ReactiveProperty<bool> Lost { get; } = new();
    public ReactiveProperty<bool> Won { get; } = new();

    public readonly IReadOnlyList<NumberViewModel> Numbers;
    private readonly NumberViewModel[,] grid = new NumberViewModel[4, 4];
    private readonly Stack<NumberViewModel> inactiveNumbers = new();
    private readonly Func<int, int> GetRandom;

    private bool moved;

    public GridViewModel(IReadOnlyList<NumberViewModel> numbers, Func<int, int> getRandom)
    {
        Numbers = numbers;
        GetRandom = getRandom;
        foreach (var number in numbers) inactiveNumbers.Push(number);
    }

    private void AddNumber()
    {
        for (int y = 0, skip = GetRandom(inactiveNumbers.Count - 1); y < grid.GetLength(0); ++y) // get the total available grid slots and pick a random one
        {
            for (var x = 0; x < grid.GetLength(1); ++x)
            {
                if (grid[x, y] != null || skip-- > 0) continue;
                var number = inactiveNumbers.Pop();
                grid[x, y] = number;
                number.SetActive(x, y);
                return;
            }
        }
    }

    private void RemoveNumber(NumberViewModel number)
    {
        inactiveNumbers.Push(number);
        grid[number.X.Value, number.Y.Value] = null;
        number.Visible.Value = false;
    }

    private void RemoveNumber(int x, int y) => RemoveNumber(grid[x, y]);

    public void Start() // add 2 numbers to start
    {
        for (var i = 0; i < 2; ++i) AddNumber();
    }

    private void NextRound()
    {
        if (!moved) return;
        AddNumber();
        foreach (var number in Numbers) number.Merged = false;
        Lost.Value = CheckLost();
        Won.Value = CheckWon();
        moved = false;
    }

    public void Reset()
    {
        Lost.Value = false;
        Won.Value = false;
        moved = false;
        foreach (var number in grid) if (number != null) RemoveNumber(number);
        Start();
    }

    public void Right()
    {
        foreach (var number in Numbers.AsValueEnumerable().
                     Where(x => x.Visible.Value && x.X.Value < grid.GetLength(0) - 1).OrderByDescending(x => x.X.Value))
        {
            for (int x = number.X.Value, y = number.Y.Value; x < grid.GetLength(0) - 1; ++x)
            {
                if (!MoveX(number, x, y, x + 1)) break;
            }
        }
        NextRound();
    }

    public void Left()
    {
        foreach (var number in Numbers.AsValueEnumerable().Where(x => x.Visible.Value && x.X.Value > 0).OrderBy(x => x.X.Value))
        {
            for (int x = number.X.Value, y = number.Y.Value; x > 0; --x)
            {
                if (!MoveX(number, x, y, x - 1)) break;
            }
        }
        NextRound();
    }

    public void Up()
    {
        foreach (var number in Numbers.AsValueEnumerable().Where(x => x.Visible.Value && x.Y.Value > 0).OrderBy(x => x.Y.Value))
        {
            for (int y = number.Y.Value, x = number.X.Value; y > 0; --y)
            {
                if (!MoveY(number, x, y, y - 1)) break;
            }
        }
        NextRound();
    }

    public void Down()
    {
        foreach (var number in Numbers.AsValueEnumerable().
                     Where(x => x.Visible.Value && x.Y.Value < grid.GetLength(1) - 1).OrderByDescending(x => x.Y.Value))
        {
            for (int y = number.Y.Value, x = number.X.Value; y < grid.GetLength(1) - 1; ++y)
            {
                if (!MoveY(number, x, y, y + 1)) break;
            }
        }
        NextRound();
    }

    private bool MoveX(NumberViewModel number, int x, int y, int newX)
    {
        if (grid[newX, y] == null)
        {
            grid[newX, y] = number;
            grid[x, y] = null;
            number.X.Value = newX;
            moved = true;
            return true;
        }
        if (grid[newX, y].Index.Value == number.Index.Value && !grid[newX, y].Merged)
        {
            RemoveNumber(number);
            ++grid[newX, y].Index.Value;
            moved = true;
            return true;
        }
        return false;
    }

    private bool MoveY(NumberViewModel number, int x, int y, int newY)
    {
        if (grid[x, newY] == null)
        {
            grid[x, newY] = number;
            grid[x, y] = null;
            number.Y.Value = newY;
            moved = true;
            return true;
        }
        if (grid[x, newY].Index.Value == number.Index.Value && !grid[x, newY].Merged)
        {
            RemoveNumber(number);
            ++grid[x, newY].Index.Value;
            moved = true;
            return true;
        }
        return false;
    }

    private bool CheckLost()
    {
        if (inactiveNumbers.Count > 0) return false;
        for (var y = 1; y < grid.GetLength(0); ++y)
        {
            for (var x = 1; x < grid.GetLength(1); ++x)
            {
                if (grid[x, y].Index.Value == grid[x - 1, y].Index.Value || grid[x, y].Index.Value == grid[x, y - 1].Index.Value) return false;
            }
        }
        return true;
    }


    private bool CheckWon() => Won.Value // no need to keep checking if already won
                               || Numbers.AsValueEnumerable().Where(x => x.Visible.Value).Any(x => x.Value >= 2048);
}
