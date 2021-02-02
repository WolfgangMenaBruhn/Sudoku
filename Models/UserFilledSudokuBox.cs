using Sudoku.Contracts.Models;

namespace Sudoku.Models
{
    public class UserFilledSudokuBox : SudokuBoxBase, IUserFilledSudokuBox
    {
        public UserFilledSudokuBox(SudokuBoxCoordinate coordinate, SudokuBoxCoordinate parentCoordinate, SudokuBoxNumbers? number)
            : base(coordinate, parentCoordinate, SudokuBoxState.UserFilled)
        {
            Number = number;
        }

        public SudokuBoxNumbers? Number { get; }

        public UserFilledSudokuBox WithNumber(SudokuBoxNumbers newNumber)
        {
            // ReSharper disable once PossibleInvalidOperationException
            return newNumber == Number ? this : new UserFilledSudokuBox(Coordinate, ParentCoordinate, newNumber);
        }

        // ReSharper disable once PossibleInvalidOperationException
        public new SudokuBoxCoordinate ParentCoordinate => base.ParentCoordinate.Value;
    }
}