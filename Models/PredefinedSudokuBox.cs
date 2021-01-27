using Sudoku.Contracts.Models;

namespace Sudoku.Models
{
    public class PredefinedSudokuBox : SudokuBoxBase, IPredefinedSudokuBox
    {
        public PredefinedSudokuBox(SudokuBoxCoordinate coordinate, SudokuBoxNumbers number) : base(coordinate, SudokuBoxState.Predefined)
        {
            Number = number;
        }

        public SudokuBoxNumbers Number { get; }
    }
}