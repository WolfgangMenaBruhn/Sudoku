using System.Collections.Generic;
using Sudoku.Contracts.Models;

namespace Sudoku.Models
{
    public class NoteSudokuBox : SudokuBoxBase, INoteSudokuBox
    {
        public NoteSudokuBox(SudokuBoxCoordinate coordinate, SudokuBoxCoordinate parentCoordinate, IEnumerable<SudokuBoxNumbers> numbers) 
            : base(coordinate, parentCoordinate, SudokuBoxState.Note)
        {
            Numbers = numbers;
        }

        public IEnumerable<SudokuBoxNumbers> Numbers { get; }
    }
}