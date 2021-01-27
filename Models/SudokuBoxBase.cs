using Catel.Data;
using Sudoku.Contracts;
using Sudoku.Contracts.Models;

namespace Sudoku.Models
{
    public abstract class SudokuBoxBase : ModelBase, ISudokuBoxBase
    {
        protected SudokuBoxBase(SudokuBoxCoordinate coordinate, SudokuBoxState state)
        {
            Coordinate = coordinate;
            State = state;
        }

        public SudokuBoxState State { get; }

        public SudokuBoxCoordinate Coordinate { get; }
    }
}