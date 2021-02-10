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

        public void SelectSingleNumber(IEnumerable<SudokuBoxNumbers> numbers)
        {
            if (Numbers.Count() != 1 || !numbers.Contains(Numbers.First())) return;
            SelectNumber(Numbers.First(), true);
        }

        public bool RemoveSingleNumbers(IEnumerable<SudokuBoxNumbers> singleNumbers)
        {
            if (Numbers.Count() <= 1) return false;

            var numberWasRemoved = false;
            var resultingNumbers = new List<SudokuBoxNumbers>(Numbers);

            foreach (var singleNumber in singleNumbers)
            {
                if (!Numbers.Contains(singleNumber)) continue;
                SelectNumber(singleNumber, false);
                resultingNumbers.Remove(singleNumber);
                numberWasRemoved = true;
            }

            if (numberWasRemoved)
                SetNumbers(resultingNumbers);

            return numberWasRemoved;
        }

        public bool CorrectWithUniqueNumbers(IEnumerable<SudokuBoxNumbers> uniqueNumbers)
        {
            if (uniqueNumbers == null) return false;
            // ReSharper disable once PossibleMultipleEnumeration
            var uniqueNumbersList = uniqueNumbers.ToList();
            if (!uniqueNumbersList.Any()) return false;

            var resultingNumbers = new List<SudokuBoxNumbers>(Numbers);

            var foundExistentUniqueNumbers = resultingNumbers.Where(n => uniqueNumbersList.Contains(n)).ToList();
            if (foundExistentUniqueNumbers.Count() > 1) return false;
            if (!foundExistentUniqueNumbers.Any()) return false;

            SetNumbers(new List<SudokuBoxNumbers> { foundExistentUniqueNumbers.First() });
            return true;
        }

        public void SelectUniqueNumbers(IEnumerable<SudokuBoxNumbers> numbers)
        {
            var numbersList = numbers.ToList();

            foreach (var number in Numbers)
            {
                SelectNumber(number, numbersList.Contains(number));
            }

            RefreshValues();
        }

        private void SelectNumber(SudokuBoxNumbers number, bool isSelected)
        {
            switch (number)
            {
                case SudokuBoxNumbers.One:
                    NumberOneIsSelected = isSelected && Numbers.Contains(number);
                    break;
                case SudokuBoxNumbers.Two:
                    NumberTwoIsSelected = isSelected && Numbers.Contains(number);
                    break;
                case SudokuBoxNumbers.Three:
                    NumberThreeIsSelected = isSelected && Numbers.Contains(number);
                    break;
                case SudokuBoxNumbers.Four:
                    NumberFourIsSelected = isSelected && Numbers.Contains(number);
                    break;
                case SudokuBoxNumbers.Five:
                    NumberFiveIsSelected = isSelected && Numbers.Contains(number);
                    break;
                case SudokuBoxNumbers.Six:
                    NumberSixIsSelected = isSelected && Numbers.Contains(number);
                    break;
                case SudokuBoxNumbers.Seven:
                    NumberSevenIsSelected = isSelected && Numbers.Contains(number);
                    break;
                case SudokuBoxNumbers.Eight:
                    NumberEightIsSelected = isSelected && Numbers.Contains(number);
                    break;
                case SudokuBoxNumbers.Nine:
                    NumberNineIsSelected = isSelected && Numbers.Contains(number);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(number), number, null);
            }
        }

        private bool mIsRefreshing;
        
        private new void RefreshValues()
        {
            if (mIsRefreshing) return;

            mIsRefreshing = true;

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

            // mSudokuService.CheckForFinished(); // Only for testing

            mIsRefreshing = false;
        }
    }
}
