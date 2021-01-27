using Sudoku.Models;

namespace Sudoku.Contracts.Models
{
    public interface IPredefinedSudokuBox : ISudokuBoxBase
    {
        SudokuBoxNumbers Number { get; }
    }
}