using System;
using System.Collections.Generic;
using Sudoku.Contracts.Models;
using Sudoku.Contracts.Services;
using Sudoku.Models;

namespace Sudoku.Services
{
    public class ModelsFactoryService : IModelsFactoryService
    {
        private SudokuBoxCoordinate GetSudokuBoxCoordinate(int x, int y)
        {
            if (x < 1 || x > 9) throw new ArgumentOutOfRangeException(nameof(x));
            if (y < 1 || y > 9) throw new ArgumentOutOfRangeException(nameof(y));

            return new SudokuBoxCoordinate((SudokuBoxNumbers) x, (SudokuBoxNumbers) y);
        }

        public IPredefinedSudokuBox GetPredefinedSudokuBox(
            SudokuBoxCoordinate coordinate,
            SudokuBoxCoordinate parentCoordinate,
            SudokuBoxNumbers number,
            bool isForControl = false)
        {
            return new PredefinedSudokuBox(coordinate, parentCoordinate, number, isForControl);
        }
        
        public IEnumerable<IPredefinedSudokuBox> GetNinePredefinedSudokuBoxes()
        {
            var result = new List<IPredefinedSudokuBox>();

            var dummyCoordinate = GetSudokuBoxCoordinate(1,1);
            for (var i = 0; i < 9; i++)
            {
                var currentCoordinate = GetSudokuBoxCoordinate(i / 3 + 1, i % 3 + 1);

                result.Add(
                    GetPredefinedSudokuBox(
                        currentCoordinate, 
                        dummyCoordinate, 
                        (SudokuBoxNumbers)(i + 1),
                        true));
            }

            return result;
        }

        public IEnumerable<IUserFilledSudokuBox> Get81EmptyUserDefinedSudokuBoxes()
        {
            var result = new List<IUserFilledSudokuBox>();

            for (var parentIndex = 0; parentIndex < 9; parentIndex++)
            {
                var parentCoordinate = GetSudokuBoxCoordinate(parentIndex / 3 + 1, parentIndex % 3 + 1);

                for (var childIndex = 0; childIndex < 9; childIndex++)
                {
                    var childCoordinate = GetSudokuBoxCoordinate(childIndex / 3 + 1, childIndex % 3 + 1);
                    
                    result.Add(
                        new UserFilledSudokuBox(
                            childCoordinate,
                            parentCoordinate,
                            null));
                }
            }

            return result;
        }
    }
}