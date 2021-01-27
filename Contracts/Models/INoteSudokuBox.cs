using System.Collections.Generic;
using Sudoku.Models;

namespace Sudoku.Contracts.Models
{
    public interface INoteSudokuBox : ISudokuBoxBase
    {
        IEnumerable<SudokuBoxNumbers> Numbers { get; }
    }
}