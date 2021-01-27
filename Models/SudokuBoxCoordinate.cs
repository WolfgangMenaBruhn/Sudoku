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

    }
}