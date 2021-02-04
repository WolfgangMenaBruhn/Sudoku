using System.Collections.Generic;
using System.Linq;
using Sudoku.Contracts.Models;

namespace Sudoku.Models
{
    public class NoteSudokuBox : SudokuBoxBase, INoteSudokuBox
    {
        public NoteSudokuBox(
            SudokuBoxCoordinate coordinate, 
            SudokuBoxCoordinate parentCoordinate,
            IEnumerable<SudokuBoxNumbers> numbers) 
            : base(coordinate, parentCoordinate, SudokuBoxState.Note)
        {
            Numbers = numbers;
        }

        public IEnumerable<SudokuBoxNumbers> Numbers { get; }

        // ReSharper disable once PossibleInvalidOperationException
        public new SudokuBoxCoordinate ParentCoordinate => base.ParentCoordinate.Value;

        public INoteSudokuBox WithOrWithoutNumber(SudokuBoxNumbers number)
        {
            var numbers = new List<SudokuBoxNumbers>(Numbers);
            if (numbers.All(n => n != number))
                numbers.Add(number);
            else
                numbers.Remove(number);

            // ReSharper disable once PossibleInvalidOperationException
            return new NoteSudokuBox(Coordinate, ParentCoordinate, numbers);
        }
    }
}