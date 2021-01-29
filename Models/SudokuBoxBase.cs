using Catel.Data;
using Sudoku.Contracts.Models;

namespace Sudoku.Models
{
    public abstract class SudokuBoxBase : ModelBase, ISudokuBoxBase
    {
        protected SudokuBoxBase(SudokuBoxCoordinate coordinate, SudokuBoxCoordinate parentCoordinate, SudokuBoxState state)
        {
            Coordinate = coordinate;
            ParentCoordinate = parentCoordinate;
            State = state;
        }

        public SudokuBoxState State { get; }

        public SudokuBoxCoordinate Coordinate { get; }

        public SudokuBoxCoordinate ParentCoordinate { get; }
    }
}