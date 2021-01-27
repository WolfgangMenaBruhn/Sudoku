using System.Collections.Generic;
using Sudoku.Contracts;
using Sudoku.Contracts.Models;

namespace Sudoku.Models
{
    public class NoteSudokuBox : SudokuBoxBase, INoteSudokuBox
    {
        public NoteSudokuBox(SudokuBoxCoordinate coordinate, IEnumerable<SudokuBoxNumbers> numbers) : base(coordinate, SudokuBoxState.Note)
        {
            Numbers = numbers;
        }

        public IEnumerable<SudokuBoxNumbers> Numbers { get; }
    }
}