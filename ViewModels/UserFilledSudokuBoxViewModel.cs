using System;
using Catel.MVVM;
using Sudoku.Contracts.Models;
using Sudoku.Contracts.Services;

namespace Sudoku.ViewModels
{
    public class UserFilledSudokuBoxViewModel : BaseSudokuBoxViewModel
    {
        private readonly ISudokuService mSudokuService;

        public UserFilledSudokuBoxViewModel(
            IUserFilledSudokuBox model,
            ISudokuService sudokuService)
        {
            mSudokuService = sudokuService;
            Model = model ?? throw new ArgumentNullException(nameof(model));
        }

        [Model]
        public IUserFilledSudokuBox Model
        {
            get => mModel as IUserFilledSudokuBox;
            set => mModel = value;
        }

        public string Number => Model.Number == null ? string.Empty : ((int) Model.Number).ToString();

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
            mSudokuService.ChangeUserDefinedNumberToPredefinedNumber(Model);
        }

        #endregion

        // ReSharper disable once UnusedMember.Local
        private new void RefreshValues()
        {
            base.RefreshValues();
            RaisePropertyChanged(nameof(Number));
        }
    }
}
