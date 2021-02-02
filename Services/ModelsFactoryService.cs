using System;
using System.Collections.Generic;
using Sudoku.Contracts.Models;
using Sudoku.Contracts.Services;
using Sudoku.Models;

namespace Sudoku.Services
{
    public class ModelsFactoryService : IModelsFactoryService
    {
        private readonly SudokuBoxCoordinate mMax3x3Coordinate = SudokuBoxCoordinate.GetMaxCoordinate3x3();

        public IPredefinedSudokuBox GetPredefinedSudokuBox(
            SudokuBoxCoordinate coordinate,
            SudokuBoxCoordinate parentCoordinate,
            SudokuBoxNumbers number,
            bool isForControl = false)
        {
            return new PredefinedSudokuBox(coordinate, parentCoordinate, number, isForControl);
        }

        public IUserFilledSudokuBox GetUserDefinedSudokuBox(
            SudokuBoxCoordinate coordinate,
            SudokuBoxCoordinate parentCoordinate,
            SudokuBoxNumbers? number)
        {
            return new UserFilledSudokuBox(coordinate, parentCoordinate, number);
        }
        
        public IEnumerable<IPredefinedSudokuBox> GetNinePredefinedSudokuBoxes()
        {
            var result = new List<IPredefinedSudokuBox>();

            var dummyCoordinate = SudokuBoxCoordinate.GetCoordinate(1,1, mMax3x3Coordinate);

            for (var index = 0; index < 9; index++)
            {
                var currentCoordinate = SudokuBoxCoordinate.GetCoordinate(index, mMax3x3Coordinate.X);
                if (!currentCoordinate.HasValue) throw new ArgumentOutOfRangeException(nameof(currentCoordinate));

                result.Add(
                    GetPredefinedSudokuBox(
                        currentCoordinate.Value, 
                        dummyCoordinate, 
                        (SudokuBoxNumbers)(index + 1),
                        true));
            }

            return result;
        }

        public IEnumerable<IUserFilledSudokuBox> Get81EmptyUserDefinedSudokuBoxes()
        {
            var result = new List<IUserFilledSudokuBox>();

            for (var parentIndex = 0; parentIndex < 9; parentIndex++)
            {
                var parentCoordinate = SudokuBoxCoordinate.GetCoordinate(parentIndex, mMax3x3Coordinate.X);
                if (!parentCoordinate.HasValue) throw new ArgumentOutOfRangeException(nameof(parentCoordinate));

                for (var childIndex = 0; childIndex < 9; childIndex++)
                {
                    var childCoordinate = SudokuBoxCoordinate.GetCoordinate(childIndex, mMax3x3Coordinate.X);
                    if (!childCoordinate.HasValue) throw new ArgumentOutOfRangeException(nameof(childCoordinate));

                    result.Add(
                        new UserFilledSudokuBox(
                            childCoordinate.Value,
                            parentCoordinate.Value,
                            null));
                }
            }

            return result;
        }
    }
}