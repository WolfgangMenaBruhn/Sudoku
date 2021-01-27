using System;
using Catel.MVVM;
using Sudoku.Contracts.Models;

namespace Sudoku.ViewModels
{
    public class PredefinedSudokuBoxViewModel : BaseSudokuBoxViewModel
    {
        public PredefinedSudokuBoxViewModel(IPredefinedSudokuBox model)
        {
            Model = model ?? throw new ArgumentNullException(nameof(model));
        }

        [Model]
        public IPredefinedSudokuBox Model
        {
            get => mModel as IPredefinedSudokuBox;
            set => mModel = value;
        }

        public string Number => ((int) Model.Number).ToString();
    }
}
