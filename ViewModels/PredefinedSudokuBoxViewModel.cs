using System;
using System.Threading.Tasks;
using Catel.MVVM;
using Sudoku.Contracts.Models;
using Sudoku.Contracts.Services;
using Sudoku.Models;

namespace Sudoku.ViewModels
{
    public class PredefinedSudokuBoxViewModel : BaseSudokuBoxViewModel, IIsDuplicated
    {
        private readonly ISudokuService mSudokuService;

        public PredefinedSudokuBoxViewModel(
            IPredefinedSudokuBox model,
            ISudokuService sudokuService)
        {
            Model = model ?? throw new ArgumentNullException(nameof(model));
            mSudokuService = sudokuService;

            mSudokuService.PredefinedNumberChanged += OnPredefinedNumberChanged;
        }

        private void OnPredefinedNumberChanged(SudokuBoxNumbers? predefinedNumber)
        {
            if (!Model.IsForControl) return;
            
            IsSelected = predefinedNumber.HasValue && predefinedNumber.Value == Model.Number;
        }

        [Model]
        public IPredefinedSudokuBox Model
        {
            get => mModel as IPredefinedSudokuBox;
            set => mModel = value;
        }

        #region click command

        private Command mClickCommand;

        // ReSharper disable once UnusedMember.Global
        public Command ClickCommand =>
            mClickCommand ??
            (mClickCommand =
                new Command(
                    ExecuteClickCommand));

        private void ExecuteClickCommand()
        {
            if (Model != null && mSudokuService.IsAllowedSettingPredefinedNumber(Model.IsForControl))
                mSudokuService.SetPredefinedNumber(Model.Number);

            if (Model != null && mSudokuService.IsAllowedChangingPredefinedNumber(Model.IsForControl))
                mSudokuService.ChangePredefinedNumberToPredefinedNumber(Model);

            mSudokuService.SudokuBoxWasClicked(Model as SudokuBoxBase);
        }

        #endregion

        public string Number => ((int) Model.Number).ToString();

        private bool mIsDuplicated;

        public bool IsDirectDuplicated
        {
            get => mIsDuplicated;
            set
            {
                if (value == mIsDuplicated) return;
                mIsDuplicated = value;
                RefreshValues();
            }
        }

        private bool mIsIndirectDuplicated;
        public bool IsIndirectDuplicated
        {
            get => mIsIndirectDuplicated;
            set
            {
                if (value == mIsIndirectDuplicated) return;
                mIsIndirectDuplicated = value;
                RefreshValues();
            }
        }

        public bool IsDuplicated => IsDirectDuplicated || IsIndirectDuplicated;

        public void SetNumber(SudokuBoxNumbers newNumber)
        {
            if (Model.IsForControl) return; // change not allowed.
            Model = Model.WithNumber(newNumber);
            RefreshValues();
        }


        private new void RefreshValues()
        {
            base.RefreshValues();
            RaisePropertyChanged(nameof(Number));
            RaisePropertyChanged(nameof(IsDirectDuplicated));
            RaisePropertyChanged(nameof(IsIndirectDuplicated));
            RaisePropertyChanged(nameof(IsDuplicated));
        }

        protected override Task CloseAsync()
        {
            mSudokuService.PredefinedNumberChanged -= OnPredefinedNumberChanged;
            return base.CloseAsync();
        }
    }
}
