using System;
using System.Linq;
using Catel.MVVM;
using Sudoku.Contracts.Models;

namespace Sudoku.ViewModels
{
    public class NoteSudokuBoxViewModel : BaseSudokuBoxViewModel
    {
        public NoteSudokuBoxViewModel(INoteSudokuBox model)
        {
            Model = model ?? throw new ArgumentNullException(nameof(model));
        }

        [Model]
        public INoteSudokuBox Model
        {
            get => mModel as INoteSudokuBox;
            set => mModel = value;
        }

        public string NumberOne => Model.Numbers.Contains(Models.SudokuBoxNumbers.One) ? "1" : string.Empty;
        public string NumberTwo => Model.Numbers.Contains(Models.SudokuBoxNumbers.Two) ? "2" : string.Empty;
        public string NumberThree => Model.Numbers.Contains(Models.SudokuBoxNumbers.Three) ? "3" : string.Empty;
        public string NumberFour => Model.Numbers.Contains(Models.SudokuBoxNumbers.Four) ? "4" : string.Empty;
        public string NumberFive => Model.Numbers.Contains(Models.SudokuBoxNumbers.Five) ? "5" : string.Empty;
        public string NumberSix => Model.Numbers.Contains(Models.SudokuBoxNumbers.Six) ? "6" : string.Empty;
        public string NumberSeven => Model.Numbers.Contains(Models.SudokuBoxNumbers.Seven) ? "7" : string.Empty;
        public string NumberEight => Model.Numbers.Contains(Models.SudokuBoxNumbers.Eight) ? "8" : string.Empty;
        public string NumberNine => Model.Numbers.Contains(Models.SudokuBoxNumbers.Nine) ? "9" : string.Empty;
    }
}
