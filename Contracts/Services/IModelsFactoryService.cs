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
            SudokuBoxCoordinate? parentCoordinate,
            SudokuBoxNumbers number);

        INoteSudokuBox GetNoteSudokuBox(
            SudokuBoxCoordinate coordinate,
            SudokuBoxCoordinate parentCoordinate,
            IEnumerable<SudokuBoxNumbers> noteNumbers);

        (SudokuBoxCoordinate? parentCoordinate, SudokuBoxCoordinate? coordinate) EmptyCoordinates();

        IUserFilledSudokuBox GetUserDefinedSudokuBox(
            SudokuBoxCoordinate coordinate,
            SudokuBoxCoordinate parentCoordinate,
            SudokuBoxNumbers? number);

        IEnumerable<IUserFilledSudokuBox> Get81EmptyUserDefinedSudokuBoxes();

        IEnumerable<SudokuBoxNumbers> SudokuNumbers();
    }
}