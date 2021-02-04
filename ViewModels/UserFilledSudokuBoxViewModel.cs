using System;
using Catel.MVVM;
using Sudoku.Contracts.Models;
using Sudoku.Contracts.Services;
using Sudoku.Models;

namespace Sudoku.ViewModels
{
    public class UserFilledSudokuBoxViewModel : BaseSudokuBoxViewModel, IIsDuplicated
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

        private bool mIsDuplicated;

        public bool IsDirectDuplicated
        {
            get => mIsDuplicated && !string.IsNullOrEmpty(Number);
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
            if (mSudokuService.IsAllowedChangingUserDefinedNumberToPredefinedNumber())
                mSudokuService.ChangeUserDefinedNumberToPredefinedNumber(Model);

            if (mSudokuService.IsAllowedChangingUserDefinedNumberToNotes())
                mSudokuService.ChangeUserDefinedNumberToNotes(Model);

            if (mSudokuService.IsAllowedSettingUserDefinedNumber())
            {
                if (mSudokuService.PredefinedNumber.HasValue)
                {
                    Model = Model.WithNumber(mSudokuService.PredefinedNumber.Value);
                    RefreshValues();

                    mSudokuService.CheckForFinished();
                }
            }

            mSudokuService.SudokuBoxWasClicked(Model as SudokuBoxBase);
        }

        #endregion

        // ReSharper disable once UnusedMember.Local
        private new void RefreshValues()
        {
            base.RefreshValues();
            RaisePropertyChanged(nameof(Number));
            RaisePropertyChanged(nameof(IsDirectDuplicated));
            RaisePropertyChanged(nameof(IsIndirectDuplicated));
            RaisePropertyChanged(nameof(IsDuplicated));
        }
    }
}
