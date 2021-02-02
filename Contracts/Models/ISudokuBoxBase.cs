using Sudoku.Models;

namespace Sudoku.Contracts.Models
{
    public interface ISudokuBoxBase
    {
        SudokuBoxState State { get; }

        SudokuBoxCoordinate Coordinate { get; }

        SudokuBoxCoordinate? ParentCoordinate { get; }
    }
}