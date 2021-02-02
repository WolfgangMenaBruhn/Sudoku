using System;

namespace Sudoku.Models
{
    public struct SudokuBoxCoordinate
    {
        public SudokuBoxCoordinate(SudokuBoxNumbers x, SudokuBoxNumbers y)
        {
            X = x;
            Y = y;
        }

        public SudokuBoxNumbers X { get; }
        
        public SudokuBoxNumbers Y { get; }

        public override bool Equals(object other)
        {
            if (!(other is SudokuBoxCoordinate otherCoordinate)) return false;
            return otherCoordinate.X == X && otherCoordinate.Y == Y;
        }

        public SudokuBoxCoordinate? GetNext(SudokuBoxCoordinate maxCoordinate)
        {
            var currentIndex = GetIndex(this, maxCoordinate.X);
            var maxIndex = GetIndex(maxCoordinate, maxCoordinate.X);

            if (currentIndex >= maxIndex) return null;

            return GetCoordinate(currentIndex + 1, maxCoordinate.X);
        }

        public static int GetIndex(SudokuBoxCoordinate coordinate, SudokuBoxNumbers xWidth)
        {
            return ((int)coordinate.X - 1) * (int)xWidth + (int)coordinate.Y - 1;
        }

        public static SudokuBoxCoordinate GetCoordinate(int x, int y, SudokuBoxCoordinate maxCoordinate)
        {
            if (x < 1 || x > (int)maxCoordinate.X) throw new ArgumentOutOfRangeException(nameof(x));
            if (y < 1 || y > (int)maxCoordinate.Y) throw new ArgumentOutOfRangeException(nameof(y));
            return new SudokuBoxCoordinate((SudokuBoxNumbers)x, (SudokuBoxNumbers)y);
        }

        public static SudokuBoxCoordinate GetMaxCoordinate3x3() =>
            new SudokuBoxCoordinate(SudokuBoxNumbers.Three, SudokuBoxNumbers.Three);

        public static SudokuBoxCoordinate GetMaxCoordinate9x9() =>
            new SudokuBoxCoordinate(SudokuBoxNumbers.Nine, SudokuBoxNumbers.Nine);

        public static SudokuBoxCoordinate? GetCoordinate(int index, SudokuBoxNumbers xWidth)
        {
            var x = index / (int)xWidth + 1;
            if (!Enum.IsDefined(typeof(SudokuBoxNumbers), x)) return null;

            var y = index % (int)xWidth + 1;
            if (!Enum.IsDefined(typeof(SudokuBoxNumbers), y)) return null;

            return new SudokuBoxCoordinate((SudokuBoxNumbers) x, (SudokuBoxNumbers) y);
        }

        public override int GetHashCode()
        {
            return (X, Y).GetHashCode();
        }
    }
}