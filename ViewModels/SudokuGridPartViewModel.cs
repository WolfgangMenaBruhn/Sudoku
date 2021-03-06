﻿using System;
using System.Collections.Generic;
using System.Linq;
using Catel.MVVM;
using System.Threading.Tasks;
using Sudoku.Contracts.Models;
using Sudoku.Contracts.Services;
using Sudoku.Models;

namespace Sudoku.ViewModels
{
    public class SudokuGridPartViewModel : ViewModelBase
    {
        private readonly List<BaseSudokuBoxViewModel> mSudokuBoxViewModels = new List<BaseSudokuBoxViewModel>(81);
        private readonly IEnumerable<ISudokuBoxBase> mSudokuBoxes;
        private readonly ISudokuService mSudokuService;
        private readonly IModelsFactoryService mModelsFactoryService;
        
        public SudokuGridPartViewModel(
            IEnumerable<ISudokuBoxBase> sudokuBoxes,
            ISudokuService sudokuService,
            IModelsFactoryService modelsFactoryService)
        {
            if (sudokuBoxes == null) throw new ArgumentNullException(nameof(sudokuBoxes));
            var sudokuBoxArray = sudokuBoxes as ISudokuBoxBase[] ?? sudokuBoxes.ToArray();
            if (sudokuBoxArray.Length != (int)SudokuBoxNumbers.Nine) throw new ArgumentOutOfRangeException(nameof(sudokuBoxes));
            
            mSudokuBoxes = sudokuBoxArray;
            mSudokuService = sudokuService;
            mModelsFactoryService = modelsFactoryService;

            CheckSudokuBoxes();
            InitializeSudokuBoxViewModels();
            mSudokuService.ChangeUserDefinedToPredefinedNumberRequest += OnChangeUserDefinedToPredefinedNumberRequested;
            mSudokuService.ChangePredefinedToPredefinedNumberRequest += OnChangePredefinedToPredefinedNumberRequested;
            mSudokuService.InformAboutClickedSudokuBox += OnSudokuBoxWasClicked;
            mSudokuService.DeletePredefinedNumberRequest += OnDeletePredefinedNumberRequested;
            mSudokuService.ChangeUserDefinedToNotesRequested += OnChangeUserDefinedToNotesRequested;
            mSudokuService.ChangeNotesToUserDefinedRequest += OnChangeNotesToUserDefinedRequest;
            mSudokuService.RefreshNotesRequested += OnRefreshUserNotesRequested;
        }

        public void SelectSingleAndUniqueNoteNumbers(IEnumerable<NoteSudokuBoxViewModel> viewModels)
        {
            bool singleNumberHasBeenRemoved;            
            var viewModelsList = viewModels.ToList();

            do
            {
                singleNumberHasBeenRemoved = false;
                var singleNoteNumbers = GetAllSingleNoteNumbers(viewModelsList).ToList();
                var uniqueNoteNumbers = GetAllUniqueNoteNumbers(viewModelsList).Except(singleNoteNumbers).ToList();

                foreach (var viewModel in viewModelsList)
                {
                    singleNumberHasBeenRemoved = singleNumberHasBeenRemoved || viewModel.RemoveSingleNumbers(singleNoteNumbers);
                    viewModel.SelectUniqueNumbers(uniqueNoteNumbers);
                    viewModel.SelectSingleNumber(singleNoteNumbers);
                }

            } while (singleNumberHasBeenRemoved);
        }

        public IEnumerable<NoteSudokuBoxViewModel> GetAllNoteViewModels()
        {
            var result = new List<NoteSudokuBoxViewModel>();

            foreach (var viewModel in mSudokuBoxViewModels)
            {
                if (!(viewModel is NoteSudokuBoxViewModel noteViewModel)) continue;
                result.Add(noteViewModel);
            }

            return result;
        }

        private IEnumerable<SudokuBoxNumbers> GetAllUniqueNoteNumbers(IEnumerable<BaseSudokuBoxViewModel> viewModels)
        {
            return
                GetAllNoteNumbers(viewModels)
                    .ToList()
                    .GroupBy(n => n)
                    .Where(g => g.Count() == 1)
                    .Select(x => x.Key)
                    .ToList();
        }

        private IEnumerable<SudokuBoxNumbers> GetAllSingleNoteNumbers(IEnumerable<BaseSudokuBoxViewModel> viewModels)
        {
            var result = new List<SudokuBoxNumbers>();

            foreach (var viewModel in viewModels)
            {
                if (!(viewModel is NoteSudokuBoxViewModel noteViewModel)) continue;
                var numbers = noteViewModel.Numbers.ToList();
                if (numbers.Count == 1) result.Add(numbers.First());
            }

            return result;
        }

        private IEnumerable<SudokuBoxNumbers> GetAllNoteNumbers(IEnumerable<BaseSudokuBoxViewModel> viewModels)
        {
            var allNumbers = new List<SudokuBoxNumbers>();

            foreach (var viewModel in viewModels)
            {
                if (!(viewModel is NoteSudokuBoxViewModel noteViewModel)) continue;

                allNumbers.AddRange(noteViewModel.Numbers);
            }

            return allNumbers;
        }

        private void OnChangeNotesToUserDefinedRequest(
            INoteSudokuBox notesBox)
        {
            if (notesBox == null) return;

            var foundViewModel =
                FindViewModel(
                    notesBox.Coordinate, notesBox.ParentCoordinate) as NoteSudokuBoxViewModel;
            if (foundViewModel == null) return;

            var viewModelIndex = mSudokuBoxViewModels.IndexOf(foundViewModel);
            var newPredefinedViewModel =
                new UserFilledSudokuBoxViewModel(
                    mModelsFactoryService.GetUserDefinedSudokuBox(
                        notesBox.Coordinate,
                        notesBox.ParentCoordinate,
                        null),
                    mSudokuService);

            mSudokuBoxViewModels.RemoveAt(viewModelIndex);
            mSudokuBoxViewModels.Insert(viewModelIndex, newPredefinedViewModel);

            RefreshValues();
        }

        private void OnChangeUserDefinedToNotesRequested(
            IUserFilledSudokuBox userFilledSudokuBox)
        {
            if (userFilledSudokuBox == null) return;

            var foundViewModel = FindViewModel(userFilledSudokuBox.Coordinate, userFilledSudokuBox.ParentCoordinate) as UserFilledSudokuBoxViewModel;
            if (foundViewModel == null) return;

            // Replace user defined view model with predefined view model:
            var viewModelIndex = mSudokuBoxViewModels.IndexOf(foundViewModel);

            var numbersInSameGrid =
                GetAllNumbers((userFilledSudokuBox.ParentCoordinate, userFilledSudokuBox.Coordinate));

            var remainingNumbers =
                mModelsFactoryService.SudokuNumbers().Except(numbersInSameGrid);

            var numbersInOtherGrids =
                mSudokuService.GetExistentNumbers(
                    (userFilledSudokuBox.ParentCoordinate, userFilledSudokuBox.Coordinate));

            if (numbersInOtherGrids != null)
                remainingNumbers = remainingNumbers.Except(numbersInOtherGrids);

            var newNoteViewModel =
                new NoteSudokuBoxViewModel(
                    mModelsFactoryService.GetNoteSudokuBox(
                        userFilledSudokuBox.Coordinate,
                        userFilledSudokuBox.ParentCoordinate,
                        new List<SudokuBoxNumbers>(remainingNumbers)),
                    mSudokuService);

            mSudokuBoxViewModels.RemoveAt(viewModelIndex);
            mSudokuBoxViewModels.Insert(viewModelIndex, newNoteViewModel);

            RefreshValues();
            SelectSingleAndUniqueNoteNumbers(GetAllNoteViewModels());
        }

        private void OnRefreshUserNotesRequested()
        {
            foreach (var viewModel in mSudokuBoxViewModels)
            {
                if (!(viewModel is NoteSudokuBoxViewModel noteViewModel)) continue;

                var model = noteViewModel.Model;

                var numbersInSameGrid =
                    GetAllNumbers((model.ParentCoordinate, model.Coordinate));

                var remainingNumbers =
                    mModelsFactoryService.SudokuNumbers().Except(numbersInSameGrid);

                var numbersInOtherGrids =
                    mSudokuService.GetExistentNumbers(
                        (model.ParentCoordinate, model.Coordinate));
                
                if (numbersInOtherGrids != null) 
                    remainingNumbers = remainingNumbers.Except(numbersInOtherGrids);

                noteViewModel.SetNumbers(remainingNumbers);
            }

            SelectSingleAndUniqueNoteNumbers(GetAllNoteViewModels());
        }

        private void CheckSudokuBoxes()
        {
            var firstBox = mSudokuBoxes.First();
            var allMustHaveParentCoordinates = firstBox.ParentCoordinate != null;
            var allMustHaveNullParentCoordinates = !allMustHaveParentCoordinates;

            foreach(var box in mSudokuBoxes)
                if (allMustHaveParentCoordinates && box.ParentCoordinate == null)
                    throw new ArgumentOutOfRangeException(nameof(box.ParentCoordinate));
                else if (allMustHaveNullParentCoordinates && box.ParentCoordinate != null)
                    throw new ArgumentOutOfRangeException(nameof(box.ParentCoordinate));
                else if (allMustHaveParentCoordinates && !box.ParentCoordinate.Value.Equals(firstBox.ParentCoordinate.Value))
                    throw new ArgumentOutOfRangeException(nameof(box.ParentCoordinate));
        }

        public SudokuBoxCoordinate? ParentCoordinate => mSudokuBoxes.First().ParentCoordinate;

        private void OnDeletePredefinedNumberRequested(IUserFilledSudokuBox sudokuBox)
        {
            if (sudokuBox == null) return;

            var foundViewModel = 
                FindViewModel(
                    sudokuBox.Coordinate, 
                    sudokuBox.ParentCoordinate
                ) as PredefinedSudokuBoxViewModel;

            if (foundViewModel?.Model == null || foundViewModel.Model.IsForControl)
                return;

            var viewModelIndex = mSudokuBoxViewModels.IndexOf(foundViewModel);

            var newUserDefinedViewModel =
                new UserFilledSudokuBoxViewModel(
                    mModelsFactoryService.GetUserDefinedSudokuBox(
                        sudokuBox.Coordinate,
                        sudokuBox.ParentCoordinate,
                        null),
                    mSudokuService);

            mSudokuBoxViewModels.RemoveAt(viewModelIndex);
            mSudokuBoxViewModels.Insert(viewModelIndex, newUserDefinedViewModel);

            RefreshValues();
        }

        private void OnSudokuBoxWasClicked(SudokuBoxBase clickedSudokuBox)
        {
            if (clickedSudokuBox == null) return;

            foreach (var viewModel in mSudokuBoxViewModels)
            {
                var currentModel = viewModel.GetModel();
                if (clickedSudokuBox is IPredefinedSudokuBox clickedPredefinedSudokuBoxModel &&
                    clickedPredefinedSudokuBoxModel.IsForControl)
                {
                    if (currentModel is IPredefinedSudokuBox currentPredefinedSudokuBoxModel &&
                        currentPredefinedSudokuBoxModel.IsForControl)
                        viewModel.IsSelected =
                            currentModel.Coordinate.Equals(clickedSudokuBox.Coordinate);
                }
                else
                {
                    var currentPredefinedSudokuBoxModel = currentModel as IPredefinedSudokuBox;
                    if (currentPredefinedSudokuBoxModel != null && currentPredefinedSudokuBoxModel.IsForControl)
                        continue;

                    viewModel.IsSelected =
                        currentModel.Coordinate.Equals(clickedSudokuBox.Coordinate) &&
                        currentModel.ParentCoordinate.Equals(clickedSudokuBox.ParentCoordinate);

                    var isHighlighted = currentModel.ParentCoordinate.Equals(clickedSudokuBox.ParentCoordinate);
                    var isSameNumber = false;

                    if (currentModel.ParentCoordinate.HasValue && clickedSudokuBox.ParentCoordinate.HasValue &&
                        currentModel.ParentCoordinate.Value.X == clickedSudokuBox.ParentCoordinate.Value.X &&
                        currentModel.Coordinate.X == clickedSudokuBox.Coordinate.X)
                        isHighlighted = true;

                    if (currentModel.ParentCoordinate.HasValue && clickedSudokuBox.ParentCoordinate.HasValue &&
                        currentModel.ParentCoordinate.Value.Y == clickedSudokuBox.ParentCoordinate.Value.Y &&
                        currentModel.Coordinate.Y == clickedSudokuBox.Coordinate.Y)
                        isHighlighted = true;

                    var currentUserDefinedBox = currentModel as IUserFilledSudokuBox;
                    if (clickedSudokuBox is IPredefinedSudokuBox clickedPredefinedBox)
                    {
                        if (currentPredefinedSudokuBoxModel != null &&
                            currentPredefinedSudokuBoxModel.Number == clickedPredefinedBox.Number)
                        {
                            isHighlighted = true;
                            isSameNumber = true;
                        }

                        if (currentUserDefinedBox?.Number != null &&
                            currentUserDefinedBox.Number.Value == clickedPredefinedBox.Number)
                        {
                            isHighlighted = true;
                            isSameNumber = true;
                        }
                    }

                    if (clickedSudokuBox is IUserFilledSudokuBox clickedUserDefinedBox && clickedUserDefinedBox.Number.HasValue)
                    {
                        if (currentPredefinedSudokuBoxModel != null &&
                            currentPredefinedSudokuBoxModel.Number == clickedUserDefinedBox.Number.Value)
                        {
                            isHighlighted = true;
                            isSameNumber = true;
                        }

                        if (currentUserDefinedBox?.Number != null &&
                            currentUserDefinedBox.Number.Value == clickedUserDefinedBox.Number.Value)
                        {
                            isHighlighted = true;
                            isSameNumber = true;
                        }
                    }

                    viewModel.IsHighlighted = isHighlighted;
                    viewModel.IsSameNumber = isSameNumber;
                }
            }

            RefreshValues();
        }

        private void OnChangeUserDefinedToPredefinedNumberRequested(
            IUserFilledSudokuBox userFilledSudokuBox,
            SudokuBoxNumbers number)
        {
            if (userFilledSudokuBox == null) return;

            var foundViewModel = FindViewModel(userFilledSudokuBox.Coordinate, userFilledSudokuBox.ParentCoordinate) as UserFilledSudokuBoxViewModel;
            if (foundViewModel == null) return;

            // Replace user defined view model with predefined view model:
            var viewModelIndex = mSudokuBoxViewModels.IndexOf(foundViewModel);

            var newPredefinedViewModel =
                new PredefinedSudokuBoxViewModel(
                    mModelsFactoryService.GetPredefinedSudokuBox(
                            userFilledSudokuBox.Coordinate,
                            userFilledSudokuBox.ParentCoordinate, 
                            number),
                    mSudokuService);

            mSudokuBoxViewModels.RemoveAt(viewModelIndex);
            mSudokuBoxViewModels.Insert(viewModelIndex, newPredefinedViewModel);

            RefreshValues();
        }

        private void OnChangePredefinedToPredefinedNumberRequested(
            IPredefinedSudokuBox predefinedSudokuBox,
            SudokuBoxNumbers number)
        {
            if (predefinedSudokuBox == null) return;

            var foundViewModel = FindViewModel(predefinedSudokuBox.Coordinate, predefinedSudokuBox.ParentCoordinate) as PredefinedSudokuBoxViewModel;
            foundViewModel?.SetNumber(number);
        }

        private BaseSudokuBoxViewModel FindViewModel(
            SudokuBoxCoordinate coordinate,
            SudokuBoxCoordinate? parentCoordinate)
        {
            foreach (var viewModel in mSudokuBoxViewModels)
            {
                var model = viewModel.GetModel();
                if (model == null) continue;

                if (model.ParentCoordinate.Equals(parentCoordinate) &&
                    model.Coordinate.Equals(coordinate))
                    return viewModel;
            }

            return null; // Nothing found
        }

        public IEnumerable<BaseSudokuBoxViewModel> FindViewModels(
            SudokuBoxNumbers number,
            bool mustHaveParentCoordinate = true)
        {
            var result = new List<BaseSudokuBoxViewModel>();

            foreach (var viewModel in mSudokuBoxViewModels)
            {
                var model = viewModel.GetModel();
                switch (model)
                {
                    case IPredefinedSudokuBox predefinedSudokuBox:
                        if (predefinedSudokuBox.Number == number)
                            if (!mustHaveParentCoordinate || predefinedSudokuBox.ParentCoordinate != null)
                                result.Add(viewModel);
                        break;
                    case IUserFilledSudokuBox userFilledSudoku:
                        if (userFilledSudoku.Number.HasValue && userFilledSudoku.Number.Value == number)
                            result.Add(viewModel);
                        break;
                }
            }

            return result;
        }

        public IEnumerable<SudokuBoxNumbers> GetAllNumbers(
            (SudokuBoxCoordinate? parentCoordinate, SudokuBoxCoordinate? coordinate) excludedCoordinates,
            bool mustHaveParentCoordinate = true)
        {
            var result = new List<SudokuBoxNumbers>();

            bool addCoordinate((SudokuBoxCoordinate? parentCoordinate, SudokuBoxCoordinate coordinate) coordinates)
            {
                var excludeCoordinates =
                    excludedCoordinates.parentCoordinate.HasValue &&
                    excludedCoordinates.coordinate.HasValue;

                if (!excludeCoordinates) return true;
                if (!coordinates.parentCoordinate.HasValue) return false;

                return
                    !(coordinates.parentCoordinate.Value.Equals(excludedCoordinates.parentCoordinate.Value) &&
                      coordinates.coordinate.Equals(excludedCoordinates.coordinate.Value));
            }

            foreach (var viewModel in mSudokuBoxViewModels)
            {
                var model = viewModel.GetModel();
                switch (model)
                {
                    case IPredefinedSudokuBox predefinedSudokuBox:
                        if (!mustHaveParentCoordinate || predefinedSudokuBox.ParentCoordinate != null)
                            if (addCoordinate((predefinedSudokuBox.ParentCoordinate, predefinedSudokuBox.Coordinate)))
                                result.Add(predefinedSudokuBox.Number);
                        break;
                    case IUserFilledSudokuBox userFilledSudoku:
                        if (userFilledSudoku.Number.HasValue)
                            if (addCoordinate((userFilledSudoku.ParentCoordinate, userFilledSudoku.Coordinate)))
                                result.Add(userFilledSudoku.Number.Value);
                        break;
                }
            }

            return result;
        }

        public void MarkDirectDuplicatedNumbers()
        {
            var duplicatedNumbers =
                GetAllNumbers(mModelsFactoryService.EmptyCoordinates())
                    .GroupBy(n => n)
                    .Where(g => g.Count() > 1)
                    .Select(x =>x.Key);

            foreach (var viewModel in mSudokuBoxViewModels)
                if (viewModel is IIsDuplicated duplicatedViewModel)
                    duplicatedViewModel.IsDirectDuplicated = false;

            foreach (var duplicatedNumber in duplicatedNumbers)
            {
                foreach (var viewModel in FindViewModels(duplicatedNumber))
                {
                    if (!(viewModel is IIsDuplicated duplicatedViewModel)) continue;
                    duplicatedViewModel.IsDirectDuplicated = true;
                }
            }

            RefreshValues();
        }

        public bool MarkIndirectDuplicatedNumbers(
            SudokuBoxCoordinate parentCoordinate,
            SudokuBoxCoordinate coordinate,
            SudokuBoxNumbers number,
            bool mark = true)
        {
            if (!ParentCoordinate.HasValue) return false;
            if (ParentCoordinate.Value.X != parentCoordinate.X && ParentCoordinate.Value.Y != parentCoordinate.Y) return false;
            
            var foundViewModels = FindViewModels(number);

            var duplicatesFound = false;

            foreach (var foundViewModel in foundViewModels)
            {
                if (!(foundViewModel is IIsDuplicated foundDuplicatableViewModel)) continue;
                var foundModel = foundViewModel.GetModel();

                if (!foundModel.ParentCoordinate.HasValue) continue;

                // ReSharper disable once PossibleInvalidOperationException
                if (foundModel.ParentCoordinate.Value.X == parentCoordinate.X && 
                    coordinate.X == foundModel.Coordinate.X)
                {
                    if (mark) foundDuplicatableViewModel.IsIndirectDuplicated = true;
                    duplicatesFound = true;
                }
                else if (foundModel.ParentCoordinate.Value.Y == parentCoordinate.Y && 
                         coordinate.Y == foundModel.Coordinate.Y)
                {
                    if (mark) foundDuplicatableViewModel.IsIndirectDuplicated = true;
                    duplicatesFound = true;
                }
            }

            if (duplicatesFound) RefreshValues();

            return duplicatesFound;
        }

        public void ResetIndirectDuplication()
        {
            foreach (var viewModel in mSudokuBoxViewModels)
            {
                if (!(viewModel is IIsDuplicated duplicatableViewModel)) continue;
                duplicatableViewModel.IsIndirectDuplicated = false;
            }
        }

        private void InitializeSudokuBoxViewModels()
        {
            mSudokuBoxViewModels.Clear();

            foreach (var sudokuBox in mSudokuBoxes)
            {
                switch (sudokuBox)
                {
                    case INoteSudokuBox noteSudokuBox:
                        mSudokuBoxViewModels.Add(new NoteSudokuBoxViewModel(noteSudokuBox, mSudokuService));
                        break;
                    case IPredefinedSudokuBox predefinedSudokuBox:
                        mSudokuBoxViewModels.Add(new PredefinedSudokuBoxViewModel(predefinedSudokuBox, mSudokuService));
                        break;
                    case IUserFilledSudokuBox userFilledSudokuBox:
                        mSudokuBoxViewModels.Add(new UserFilledSudokuBoxViewModel(userFilledSudokuBox, mSudokuService));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(sudokuBox));
                }
            }
        }

        public bool IsFinished()
        {
            foreach (var viewModel in mSudokuBoxViewModels)
            {
                if (!(viewModel is IIsDuplicated duplicatedViewModel)) return false;
                if (duplicatedViewModel.IsDuplicated) return false;

                switch (viewModel.GetModel())
                {
                    case IUserFilledSudokuBox userDefinedBox:
                        if (!userDefinedBox.Number.HasValue)
                            return false;
                        break;
                }
            }

            return true;
        }

        #region view box models

        public BaseSudokuBoxViewModel ViewBoxModel_1 => mSudokuBoxViewModels[0];
        public BaseSudokuBoxViewModel ViewBoxModel_2 => mSudokuBoxViewModels[1];
        public BaseSudokuBoxViewModel ViewBoxModel_3 => mSudokuBoxViewModels[2];
        public BaseSudokuBoxViewModel ViewBoxModel_4 => mSudokuBoxViewModels[3];
        public BaseSudokuBoxViewModel ViewBoxModel_5 => mSudokuBoxViewModels[4];
        public BaseSudokuBoxViewModel ViewBoxModel_6 => mSudokuBoxViewModels[5];
        public BaseSudokuBoxViewModel ViewBoxModel_7 => mSudokuBoxViewModels[6];
        public BaseSudokuBoxViewModel ViewBoxModel_8 => mSudokuBoxViewModels[7];
        public BaseSudokuBoxViewModel ViewBoxModel_9 => mSudokuBoxViewModels[8];

        #endregion

        private bool mIsRefreshing;
        
        private void RefreshValues()
        {
            if (mIsRefreshing) return;

            mIsRefreshing = true;

            RaisePropertyChanged(nameof(ViewBoxModel_1));
            RaisePropertyChanged(nameof(ViewBoxModel_2));
            RaisePropertyChanged(nameof(ViewBoxModel_3));
            RaisePropertyChanged(nameof(ViewBoxModel_4));
            RaisePropertyChanged(nameof(ViewBoxModel_5));
            RaisePropertyChanged(nameof(ViewBoxModel_6));
            RaisePropertyChanged(nameof(ViewBoxModel_7));
            RaisePropertyChanged(nameof(ViewBoxModel_8));
            RaisePropertyChanged(nameof(ViewBoxModel_9));

            mIsRefreshing = false;
        }

        public override string Title => "View model title";

        protected override async Task InitializeAsync()
        {
            await base.InitializeAsync();
        }

        protected override async Task CloseAsync()
        {
            mSudokuService.ChangeUserDefinedToPredefinedNumberRequest -= OnChangeUserDefinedToPredefinedNumberRequested;
            mSudokuService.ChangePredefinedToPredefinedNumberRequest -= OnChangePredefinedToPredefinedNumberRequested;
            mSudokuService.InformAboutClickedSudokuBox -= OnSudokuBoxWasClicked;
            mSudokuService.DeletePredefinedNumberRequest -= OnDeletePredefinedNumberRequested;
            mSudokuService.ChangeUserDefinedToNotesRequested -= OnChangeUserDefinedToNotesRequested;
            mSudokuService.ChangeNotesToUserDefinedRequest -= OnChangeNotesToUserDefinedRequest;
            await base.CloseAsync();
        }
    }
}
