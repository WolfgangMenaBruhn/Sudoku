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
    }
}