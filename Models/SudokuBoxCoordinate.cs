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

        public override int GetHashCode()
        {
            return (X, Y).GetHashCode();
        }
    }
}