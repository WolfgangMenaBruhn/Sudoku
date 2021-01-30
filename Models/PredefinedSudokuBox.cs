using Sudoku.Contracts.Models;

namespace Sudoku.Models
{
    public class PredefinedSudokuBox : SudokuBoxBase, IPredefinedSudokuBox
    {
        public PredefinedSudokuBox(
            SudokuBoxCoordinate coordinate, 
            SudokuBoxCoordinate parentCoordinate, 
            SudokuBoxNumbers number, 
            bool isForControl = false)
            : base(coordinate, parentCoordinate, SudokuBoxState.Predefined)
        {
            Number = number;
            IsForControl = isForControl;
        }

        public SudokuBoxNumbers Number { get; }

        public bool IsForControl { get; }

        public IPredefinedSudokuBox WithNumber(SudokuBoxNumbers newNumber)
        {
            return newNumber == Number ? this : new PredefinedSudokuBox(Coordinate, ParentCoordinate, newNumber, IsForControl);
        }
    }
}