using Sudoku.Contracts;
using Sudoku.Contracts.Models;

namespace Sudoku.Models
{
    public class UserFilledSudokuBox : SudokuBoxBase, IUserFilledSudokuBox
    {
        public UserFilledSudokuBox(SudokuBoxCoordinate coordinate, SudokuBoxNumbers? number) : base(coordinate, SudokuBoxState.UserFilled)
        {
            Number = number;
        }

        public SudokuBoxNumbers? Number { get; }
    }
}