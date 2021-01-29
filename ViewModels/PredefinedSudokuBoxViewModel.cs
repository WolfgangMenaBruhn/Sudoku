using System;
using Catel.MVVM;
using Sudoku.Contracts.Models;
using Sudoku.Contracts.Services;

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
        }

        #endregion

        public string Number => ((int) Model.Number).ToString();
    }
}
