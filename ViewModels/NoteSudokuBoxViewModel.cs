using System;
using System.Collections.Generic;
using System.Linq;
using Catel.MVVM;
using Sudoku.Contracts.Models;
using Sudoku.Contracts.Services;
using Sudoku.Models;

namespace Sudoku.ViewModels
{
    public class NoteSudokuBoxViewModel : BaseSudokuBoxViewModel
    {
        private readonly ISudokuService mSudokuService;

        public NoteSudokuBoxViewModel(
            INoteSudokuBox model,
            ISudokuService sudokuService)
        {
            mSudokuService = sudokuService;
            Model = model ?? throw new ArgumentNullException(nameof(model));
        }

        [Model]
        public INoteSudokuBox Model
        {
            get => mModel as INoteSudokuBox;
            set => mModel = value;
        }

        public IEnumerable<SudokuBoxNumbers> Numbers => Model.Numbers;

        public string NumberOne => Model.Numbers.Contains(Models.SudokuBoxNumbers.One) ? "1" : string.Empty;
        public string NumberTwo => Model.Numbers.Contains(Models.SudokuBoxNumbers.Two) ? "2" : string.Empty;
        public string NumberThree => Model.Numbers.Contains(Models.SudokuBoxNumbers.Three) ? "3" : string.Empty;
        public string NumberFour => Model.Numbers.Contains(Models.SudokuBoxNumbers.Four) ? "4" : string.Empty;
        public string NumberFive => Model.Numbers.Contains(Models.SudokuBoxNumbers.Five) ? "5" : string.Empty;
        public string NumberSix => Model.Numbers.Contains(Models.SudokuBoxNumbers.Six) ? "6" : string.Empty;
        public string NumberSeven => Model.Numbers.Contains(Models.SudokuBoxNumbers.Seven) ? "7" : string.Empty;
        public string NumberEight => Model.Numbers.Contains(Models.SudokuBoxNumbers.Eight) ? "8" : string.Empty;
        public string NumberNine => Model.Numbers.Contains(Models.SudokuBoxNumbers.Nine) ? "9" : string.Empty;

        #region click number one command

        private Command mClickNumberOneCommand;

        // ReSharper disable once UnusedMember.Global
        public Command ClickNumberOneCommand =>
            mClickNumberOneCommand ??
            (mClickNumberOneCommand =
                new Command(
                    ExecuteNumberOneClickCommand));

        private void ExecuteNumberOneClickCommand()
        {
            if (mSudokuService.IsAllowedChangingNotesToUserDefined())
            {
                mSudokuService.ChangeNotesToUserDefined(Model);
                return;
            }
            
            Model = Model.WithOrWithoutNumber(Models.SudokuBoxNumbers.One);
            RefreshValues();
        }

        #endregion

        #region click number two command

        private Command mClickNumberTwoCommand;

        // ReSharper disable once UnusedMember.Global
        public Command ClickNumberTwoCommand =>
            mClickNumberTwoCommand ??
            (mClickNumberTwoCommand =
                new Command(
                    ExecuteNumberTwoClickCommand));

        private void ExecuteNumberTwoClickCommand()
        {
            if (mSudokuService.IsAllowedChangingNotesToUserDefined())
            {
                mSudokuService.ChangeNotesToUserDefined(Model);
                return;
            }
            
            Model = Model.WithOrWithoutNumber(Models.SudokuBoxNumbers.Two);
            RefreshValues();
        }

        #endregion

        #region click number Three command

        private Command mClickNumberThreeCommand;

        // ReSharper disable once UnusedMember.Global
        public Command ClickNumberThreeCommand =>
            mClickNumberThreeCommand ??
            (mClickNumberThreeCommand =
                new Command(
                    ExecuteNumberThreeClickCommand));

        private void ExecuteNumberThreeClickCommand()
        {
            if (mSudokuService.IsAllowedChangingNotesToUserDefined())
            {
                mSudokuService.ChangeNotesToUserDefined(Model);
                return;
            }

            Model = Model.WithOrWithoutNumber(Models.SudokuBoxNumbers.Three);
            RefreshValues();
        }

        #endregion

        #region click number Four command

        private Command mClickNumberFourCommand;

        // ReSharper disable once UnusedMember.Global
        public Command ClickNumberFourCommand =>
            mClickNumberFourCommand ??
            (mClickNumberFourCommand =
                new Command(
                    ExecuteNumberFourClickCommand));

        private void ExecuteNumberFourClickCommand()
        {
            if (mSudokuService.IsAllowedChangingNotesToUserDefined())
            {
                mSudokuService.ChangeNotesToUserDefined(Model);
                return;
            }

            Model = Model.WithOrWithoutNumber(Models.SudokuBoxNumbers.Four);
            RefreshValues();
        }

        #endregion

        #region click number Five command

        private Command mClickNumberFiveCommand;

        // ReSharper disable once UnusedMember.Global
        public Command ClickNumberFiveCommand =>
            mClickNumberFiveCommand ??
            (mClickNumberFiveCommand =
                new Command(
                    ExecuteNumberFiveClickCommand));

        private void ExecuteNumberFiveClickCommand()
        {
            if (mSudokuService.IsAllowedChangingNotesToUserDefined())
            {
                mSudokuService.ChangeNotesToUserDefined(Model);
                return;
            }

            Model = Model.WithOrWithoutNumber(Models.SudokuBoxNumbers.Five);
            RefreshValues();
        }

        #endregion

        #region click number Six command

        private Command mClickNumberSixCommand;

        // ReSharper disable once UnusedMember.Global
        public Command ClickNumberSixCommand =>
            mClickNumberSixCommand ??
            (mClickNumberSixCommand =
                new Command(
                    ExecuteNumberSixClickCommand));

        private void ExecuteNumberSixClickCommand()
        {
            if (mSudokuService.IsAllowedChangingNotesToUserDefined())
            {
                mSudokuService.ChangeNotesToUserDefined(Model);
                return;
            }

            Model = Model.WithOrWithoutNumber(Models.SudokuBoxNumbers.Six);
            RefreshValues();
        }

        #endregion

        #region click number Seven command

        private Command mClickNumberSevenCommand;

        // ReSharper disable once UnusedMember.Global
        public Command ClickNumberSevenCommand =>
            mClickNumberSevenCommand ??
            (mClickNumberSevenCommand =
                new Command(
                    ExecuteNumberSevenClickCommand));

        private void ExecuteNumberSevenClickCommand()
        {
            if (mSudokuService.IsAllowedChangingNotesToUserDefined())
            {
                mSudokuService.ChangeNotesToUserDefined(Model);
                return;
            }

            Model = Model.WithOrWithoutNumber(Models.SudokuBoxNumbers.Seven);
            RefreshValues();
        }

        #endregion

        #region click number Eight command

        private Command mClickNumberEightCommand;

        // ReSharper disable once UnusedMember.Global
        public Command ClickNumberEightCommand =>
            mClickNumberEightCommand ??
            (mClickNumberEightCommand =
                new Command(
                    ExecuteNumberEightClickCommand));

        private void ExecuteNumberEightClickCommand()
        {
            if (mSudokuService.IsAllowedChangingNotesToUserDefined())
            {
                mSudokuService.ChangeNotesToUserDefined(Model);
                return;
            }

            Model = Model.WithOrWithoutNumber(Models.SudokuBoxNumbers.Eight);
            RefreshValues();
        }

        #endregion

        #region click number Nine command

        private Command mClickNumberNineCommand;

        // ReSharper disable once UnusedMember.Global
        public Command ClickNumberNineCommand =>
            mClickNumberNineCommand ??
            (mClickNumberNineCommand =
                new Command(
                    ExecuteNumberNineClickCommand));

        private void ExecuteNumberNineClickCommand()
        {
            if (mSudokuService.IsAllowedChangingNotesToUserDefined())
            {
                mSudokuService.ChangeNotesToUserDefined(Model);
                return;
            }

            Model = Model.WithOrWithoutNumber(SudokuBoxNumbers.Nine);
            RefreshValues();
        }

        #endregion

        public void SetNumbers(IEnumerable<SudokuBoxNumbers> numbers)
        {
            Model = Model.WithNumbers(numbers);
            RefreshValues();
        }

        public bool NumberOneIsSelected { get; protected set; }
        public bool NumberTwoIsSelected { get; protected set; }
        public bool NumberThreeIsSelected { get; protected set; }
        public bool NumberFourIsSelected { get; protected set; }
        public bool NumberFiveIsSelected { get; protected set; }
        public bool NumberSixIsSelected { get; protected set; }
        public bool NumberSevenIsSelected { get; protected set; }
        public bool NumberEightIsSelected { get; protected set; }
        public bool NumberNineIsSelected { get; protected set; }

        public void SelectNumbers(IEnumerable<SudokuBoxNumbers> numbers)
        {
            foreach (var number in numbers)
            {
                if (!(Numbers.Contains(number))) continue;

                switch (number)
                {
                    case SudokuBoxNumbers.One:
                        NumberOneIsSelected = true;
                        break;
                    case SudokuBoxNumbers.Two:
                        NumberTwoIsSelected = true;
                        break;
                    case SudokuBoxNumbers.Three:
                        NumberThreeIsSelected = true;
                        break;
                    case SudokuBoxNumbers.Four:
                        NumberFourIsSelected = true;
                        break;
                    case SudokuBoxNumbers.Five:
                        NumberFiveIsSelected = true;
                        break;
                    case SudokuBoxNumbers.Six:
                        NumberSixIsSelected = true;
                        break;
                    case SudokuBoxNumbers.Seven:
                        NumberSevenIsSelected = true;
                        break;
                    case SudokuBoxNumbers.Eight:
                        NumberEightIsSelected = true;
                        break;
                    case SudokuBoxNumbers.Nine:
                        NumberNineIsSelected = true;
                        break;
                }
            }

            RefreshValues();
        }

        private new void RefreshValues()
        {
            RaisePropertyChanged(nameof(NumberOne));
            RaisePropertyChanged(nameof(NumberTwo));
            RaisePropertyChanged(nameof(NumberThree));
            RaisePropertyChanged(nameof(NumberFour));
            RaisePropertyChanged(nameof(NumberFive));
            RaisePropertyChanged(nameof(NumberSix));
            RaisePropertyChanged(nameof(NumberSeven));
            RaisePropertyChanged(nameof(NumberEight));
            RaisePropertyChanged(nameof(NumberNine));

            RaisePropertyChanged(nameof(NumberOneIsSelected));
            RaisePropertyChanged(nameof(NumberTwoIsSelected));
            RaisePropertyChanged(nameof(NumberThreeIsSelected));
            RaisePropertyChanged(nameof(NumberFourIsSelected));
            RaisePropertyChanged(nameof(NumberFiveIsSelected));
            RaisePropertyChanged(nameof(NumberSixIsSelected));
            RaisePropertyChanged(nameof(NumberSevenIsSelected));
            RaisePropertyChanged(nameof(NumberEightIsSelected));
            RaisePropertyChanged(nameof(NumberNineIsSelected));
            base.RefreshValues();
        }
    }
}
