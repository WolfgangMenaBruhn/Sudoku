using Sudoku.Contracts.Models;

namespace Sudoku.Models
{
    public class PredefinedSudokuBox : SudokuBoxBase, IPredefinedSudokuBox
    {
        public PredefinedSudokuBox(
            SudokuBoxCoordinate coordinate, 
            SudokuBoxCoordinate? parentCoordinate, 
            SudokuBoxNumbers number)
            : base(coordinate, parentCoordinate, SudokuBoxState.Predefined)
        {
            Number = number;
        }

        public SudokuBoxNumbers Number { get; }

        public bool IsForControl => ParentCoordinate == null;

        public IPredefinedSudokuBox WithNumber(SudokuBoxNumbers newNumber)
        {
            return newNumber == Number ? this : new PredefinedSudokuBox(Coordinate, ParentCoordinate, newNumber);
        }
    }
}