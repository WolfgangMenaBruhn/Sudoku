﻿using System;
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
            SudokuBoxCoordinate? parentCoordinate,
            SudokuBoxNumbers number)
        {
            return new PredefinedSudokuBox(coordinate, parentCoordinate, number);
        }

        public IEnumerable<SudokuBoxNumbers> SudokuNumbers() => new List<SudokuBoxNumbers>
        {
            SudokuBoxNumbers.One, SudokuBoxNumbers.Two, SudokuBoxNumbers.Three, SudokuBoxNumbers.Four,
            SudokuBoxNumbers.Five, SudokuBoxNumbers.Six, SudokuBoxNumbers.Seven, SudokuBoxNumbers.Eight,
            SudokuBoxNumbers.Nine
        };

        public (SudokuBoxCoordinate? parentCoordinate, SudokuBoxCoordinate? coordinate) EmptyCoordinates() =>
            (null, null);

        public INoteSudokuBox GetNoteSudokuBox(
            SudokuBoxCoordinate coordinate,
            SudokuBoxCoordinate parentCoordinate,
            IEnumerable<SudokuBoxNumbers> noteNumbers)
        {
            return 
                new NoteSudokuBox(
                    coordinate, 
                    parentCoordinate, 
                    new List<SudokuBoxNumbers>(noteNumbers));
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

            for (var index = (int)SudokuBoxNumbers.One - 1; index < (int)SudokuBoxNumbers.Nine; index++)
            {
                var currentCoordinate = SudokuBoxCoordinate.GetCoordinate(index, mMax3x3Coordinate.X);
                if (!currentCoordinate.HasValue) throw new ArgumentOutOfRangeException(nameof(currentCoordinate));

                result.Add(
                    GetPredefinedSudokuBox(
                        currentCoordinate.Value, 
                        null, 
                        (SudokuBoxNumbers)(index + 1)));
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

                for (var childIndex = (int)SudokuBoxNumbers.One - 1; childIndex < (int)SudokuBoxNumbers.Nine; childIndex++)
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