using System.Collections.Generic;
using System.Linq;
using Catel.MVVM;
using System.Threading.Tasks;
using Catel.Services;
using Sudoku.Contracts.Services;
using Sudoku.Helper.Extensions;
using Sudoku.Models;

namespace Sudoku.ViewModels
{
    public class SudokuGridViewModel : ViewModelBase
    {
        private readonly IModelsFactoryService mModelsFactoryService;
        private readonly ISudokuService mSudokuService;
        private readonly IMessageService mMessageService;
        private readonly List<SudokuGridPartViewModel> mSudokuBoxViewModels = new List<SudokuGridPartViewModel>(9);

        public SudokuGridViewModel(
            IModelsFactoryService modelsFactoryService,
            ISudokuService sudokuService,
            IMessageService messageService)
        {
            mModelsFactoryService = modelsFactoryService;
            mSudokuService = sudokuService;
            mMessageService = messageService;
            InitializeSudokuBoxViewModels();

            mSudokuService.ResetRequest += OnResetRequested;
            mSudokuService.MarkDuplicatedNumbersRequested += OnMarkDuplicatedNumbersRequested;
            mSudokuService.CheckForFinishedRequested += OnCheckForFinishedRequested;
        }

        private void OnCheckForFinishedRequested()
        {
            if (!IsFinished()) return;

            mMessageService.ShowInformationAsync(
                "Game finished!",
                "Sudoku:");
        }

        private void OnMarkDuplicatedNumbersRequested()
        {
            foreach (var gridPartViewModel in mSudokuBoxViewModels)
                gridPartViewModel.MarkDirectDuplicatedNumbers();

            for (int index = (int) SudokuBoxNumbers.One - 1; index < (int) SudokuBoxNumbers.Nine; index++)
            {
                var parentViewModel = mSudokuBoxViewModels[index];
                parentViewModel.ResetIndirectDuplication();
            }
            
            for (int index = (int) SudokuBoxNumbers.One - 1; index < (int) SudokuBoxNumbers.Eight; index++)
            {
                var parentViewModel = mSudokuBoxViewModels[index];
                if (!parentViewModel.ParentCoordinate.HasValue) continue;
                
                for (int subIndex = index + 1; subIndex < (int) SudokuBoxNumbers.Nine; subIndex++)
                {
                    var childViewModel = mSudokuBoxViewModels[subIndex];
                    if (!childViewModel.ParentCoordinate.HasValue) continue;

                    var parentNumbers = parentViewModel.GetAllNumbers().Distinct().ToList();

                    foreach (var currentParentNumber in parentNumbers)
                    {
                        foreach (var currentParentViewModel in parentViewModel.FindViewModels(currentParentNumber))
                        {
                            if (!(currentParentViewModel is IIsDuplicated duplicatableParentViewModel)) continue;

                            // ReSharper disable once PossibleInvalidOperationException
                            var parentCoordinate = parentViewModel.ParentCoordinate.Value;
                            var currentCoordinate = currentParentViewModel.GetModel().Coordinate;

                            if (childViewModel.MarkIndirectDuplicatedNumbers(parentCoordinate, currentCoordinate, currentParentNumber))
                                duplicatableParentViewModel.IsIndirectDuplicated = true;
                        }
                    }
                }
            }
        }

        private void OnResetRequested()
        {
            InitializeSudokuBoxViewModels();
        }

        public bool IsFinished()
        {
            foreach (var viewModel in mSudokuBoxViewModels)
                if (!viewModel.IsFinished()) return false;

            return true;
        }

        private void InitializeSudokuBoxViewModels()
        {
            mSudokuBoxViewModels.Clear();

            var batches = mModelsFactoryService.Get81EmptyUserDefinedSudokuBoxes().Partition(9);
            foreach (var batch in batches)
                mSudokuBoxViewModels.Add(new SudokuGridPartViewModel(batch, mSudokuService, mModelsFactoryService));

            RefreshValues();
        }

        #region view box models

        public SudokuGridPartViewModel ViewBoxModel_1 => mSudokuBoxViewModels[0];
        public SudokuGridPartViewModel ViewBoxModel_2 => mSudokuBoxViewModels[1];
        public SudokuGridPartViewModel ViewBoxModel_3 => mSudokuBoxViewModels[2];
        public SudokuGridPartViewModel ViewBoxModel_4 => mSudokuBoxViewModels[3];
        public SudokuGridPartViewModel ViewBoxModel_5 => mSudokuBoxViewModels[4];
        public SudokuGridPartViewModel ViewBoxModel_6 => mSudokuBoxViewModels[5];
        public SudokuGridPartViewModel ViewBoxModel_7 => mSudokuBoxViewModels[6];
        public SudokuGridPartViewModel ViewBoxModel_8 => mSudokuBoxViewModels[7];
        public SudokuGridPartViewModel ViewBoxModel_9 => mSudokuBoxViewModels[8];

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

        protected override async Task InitializeAsync()
        {
            await base.InitializeAsync();
        }

        protected override async Task CloseAsync()
        {
            mSudokuService.ResetRequest -= OnResetRequested;
            await base.CloseAsync();
        }
    }
}
