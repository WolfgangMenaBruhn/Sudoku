using System;
using Catel.MVVM;
using Sudoku.Contracts.Models;
using Sudoku.Contracts.Services;
using Sudoku.Models;

namespace Sudoku.ViewModels
{
    public class PredefinedSudokuBoxViewModel : BaseSudokuBoxViewModel
    {
        private readonly ISudokuService mSudokuService;

        public PredefinedSudokuBoxViewModel(
            IPredefinedSudokuBox model,
            ISudokuService sudokuService)
        {
            Model = model ?? throw new ArgumentNullException(nameof(model));
            mSudokuService = sudokuService;
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
        }

        #endregion

        public string Number => ((int) Model.Number).ToString();

        public void ChangeNumber(SudokuBoxNumbers newNumber)
        {
            if (Model.IsForControl) return; // Not change allowed.
            Model = Model.WithNumber(newNumber);
            RefreshValues();
        }

        private void RefreshValues()
        {
            RaisePropertyChanged(nameof(Number));
        }
    }
}
