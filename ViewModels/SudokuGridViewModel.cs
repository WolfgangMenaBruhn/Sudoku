using System.Collections.Generic;
using Catel.MVVM;
using System.Threading.Tasks;
using Sudoku.Contracts.Services;
using Sudoku.Helper.Extensions;

namespace Sudoku.ViewModels
{
    public class SudokuGridViewModel : ViewModelBase
    {
        private readonly IModelsFactoryService mModelsFactoryService;
        private readonly ISudokuService mSudokuService;
        private readonly List<SudokuGridPartViewModel> mSudokuBoxViewModels = new List<SudokuGridPartViewModel>(9);

        public SudokuGridViewModel(
            IModelsFactoryService modelsFactoryService,
            ISudokuService sudokuService)
        {
            mModelsFactoryService = modelsFactoryService;
            mSudokuService = sudokuService;
            InitializeSudokuBoxViewModels();

            mSudokuService.ResetRequest += OnResetRequested;
        }

        private void OnResetRequested()
        {
            InitializeSudokuBoxViewModels();
            RefreshValues();
        }

        private void InitializeSudokuBoxViewModels()
        {
            mSudokuBoxViewModels.Clear();

            var batches = mModelsFactoryService.Get81EmptyUserDefinedSudokuBoxes().Partition(9);
            foreach (var batch in batches)
                mSudokuBoxViewModels.Add(new SudokuGridPartViewModel(batch, mSudokuService, mModelsFactoryService));
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
