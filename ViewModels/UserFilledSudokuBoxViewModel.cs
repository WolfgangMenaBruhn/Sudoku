using System;
using Catel.MVVM;
using Sudoku.Contracts.Models;

namespace Sudoku.ViewModels
{
    public class UserFilledSudokuBoxViewModel : BaseSudokuBoxViewModel
    {
        public UserFilledSudokuBoxViewModel(IUserFilledSudokuBox model)
        {
            Model = model ?? throw new ArgumentNullException(nameof(model));
        }

        [Model]
        public IUserFilledSudokuBox Model
        {
            get => mModel as IUserFilledSudokuBox;
            set => mModel = value;
        }

        public string Number => Model.Number == null ? string.Empty : ((int) Model.Number).ToString();
    }
}
