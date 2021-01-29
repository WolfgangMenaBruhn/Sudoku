using System.Collections.Generic;
using Sudoku.Contracts.Models;
using Sudoku.Models;

namespace Sudoku.Contracts.Services
{
    public interface IModelsFactoryService
    {
        IEnumerable<IPredefinedSudokuBox> GetNinePredefinedSudokuBoxes();

        IPredefinedSudokuBox GetPredefinedSudokuBox(
            SudokuBoxCoordinate coordinate,
            SudokuBoxCoordinate parentCoordinate,
            SudokuBoxNumbers number,
            bool isForControl = false);

        IEnumerable<IUserFilledSudokuBox> Get81EmptyUserDefinedSudokuBoxes();
    }
}