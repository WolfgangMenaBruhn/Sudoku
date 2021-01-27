using Sudoku.Models;

namespace Sudoku.Contracts.Models
{
    public interface IUserFilledSudokuBox : ISudokuBoxBase
    {
        SudokuBoxNumbers? Number { get; }
    }
}