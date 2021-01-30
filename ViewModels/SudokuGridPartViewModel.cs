using System;
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
            if (sudokuBoxArray.Count() != 9) throw new ArgumentOutOfRangeException(nameof(sudokuBoxes));
            
            mSudokuBoxes = sudokuBoxArray;
            mSudokuService = sudokuService;
            mModelsFactoryService = modelsFactoryService;

            InitializeSudokuBoxViewModels();
            mSudokuService.ChangeUserDefinedToPredefinedNumberRequest += OnChangeUserDefinedToPredefinedNumberRequested;
            mSudokuService.ChangePredefinedToPredefinedNumberRequest += OnChangePredefinedToPredefinedNumberRequested;
            mSudokuService.InformAboutClickedSudokuBox += OnSudokuBoxWasClicked;
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

                    if (currentModel.ParentCoordinate.X == clickedSudokuBox.ParentCoordinate.X &&
                        currentModel.Coordinate.X == clickedSudokuBox.Coordinate.X)
                        isHighlighted = true;

                    if (currentModel.ParentCoordinate.Y == clickedSudokuBox.ParentCoordinate.Y &&
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
        }

        private void OnChangeUserDefinedToPredefinedNumberRequested(
            IUserFilledSudokuBox userFilledSudokuBox,
            SudokuBoxNumbers number)
        {
            if (userFilledSudokuBox?.ParentCoordinate == null) return;

            var foundViewModel = FindUserDefinedViewModel(userFilledSudokuBox);
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

            var foundViewModel = FindPredefinedViewModel(predefinedSudokuBox);
            foundViewModel?.ChangeNumber(number);
        }

        private PredefinedSudokuBoxViewModel FindPredefinedViewModel(
            IPredefinedSudokuBox predefinedSudokuBox)
        {
            if (predefinedSudokuBox == null) return null;

            foreach (var viewModel in mSudokuBoxViewModels)
            {
                if (!(viewModel is PredefinedSudokuBoxViewModel predefinedSudokuBoxViewModel)) continue;
                if (predefinedSudokuBoxViewModel.Model == null) continue;

                if (predefinedSudokuBoxViewModel.Model.ParentCoordinate.Equals(predefinedSudokuBox.ParentCoordinate) &&
                    predefinedSudokuBoxViewModel.Model.Coordinate.Equals(predefinedSudokuBox.Coordinate))
                    return predefinedSudokuBoxViewModel;
            }

            return null; // Nothing found
        }

        private UserFilledSudokuBoxViewModel FindUserDefinedViewModel(
            IUserFilledSudokuBox userFilledSudokuBox)
        {
            if (userFilledSudokuBox == null) return null;

            foreach (var viewModel in mSudokuBoxViewModels)
            {
                if (!(viewModel is UserFilledSudokuBoxViewModel userDefinedSudokuBoxViewModel)) continue;
                if (userDefinedSudokuBoxViewModel.Model == null) continue;
                

                if (userDefinedSudokuBoxViewModel.Model.ParentCoordinate.Equals(userFilledSudokuBox.ParentCoordinate) &&
                    userDefinedSudokuBoxViewModel.Model.Coordinate.Equals(userFilledSudokuBox.Coordinate))
                    return userDefinedSudokuBoxViewModel;
            }

            return null; // Nothing found
        }

        private void InitializeSudokuBoxViewModels()
        {
            mSudokuBoxViewModels.Clear();

            foreach (var sudokuBox in mSudokuBoxes)
            {
                switch (sudokuBox)
                {
                    case INoteSudokuBox noteSudokuBox:
                        mSudokuBoxViewModels.Add(new NoteSudokuBoxViewModel(noteSudokuBox));
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

        private void RefreshValues()
        {
            RaisePropertyChanged(nameof(ViewBoxModel_1));
            RaisePropertyChanged(nameof(ViewBoxModel_2));
            RaisePropertyChanged(nameof(ViewBoxModel_3));
            RaisePropertyChanged(nameof(ViewBoxModel_4));
            RaisePropertyChanged(nameof(ViewBoxModel_5));
            RaisePropertyChanged(nameof(ViewBoxModel_6));
            RaisePropertyChanged(nameof(ViewBoxModel_7));
            RaisePropertyChanged(nameof(ViewBoxModel_8));
            RaisePropertyChanged(nameof(ViewBoxModel_9));
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
            await base.CloseAsync();
        }
    }
}
