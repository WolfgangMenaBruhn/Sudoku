using System;
using System.Collections.Generic;
using System.Linq;
using Catel.MVVM;
using System.Threading.Tasks;
using Sudoku.Contracts.Models;

namespace Sudoku.ViewModels
{
    public class SudokuGridViewModel : ViewModelBase
    {
        private readonly List<BaseSudokuBoxViewModel> mSudokuBoxViewModels = new List<BaseSudokuBoxViewModel>(9);
        private readonly IEnumerable<ISudokuBoxBase> mSudokuBoxes;

        public SudokuGridViewModel(IEnumerable<ISudokuBoxBase> sudokuBoxes)
        {
            if (sudokuBoxes == null) throw new ArgumentNullException(nameof(sudokuBoxes));
            var sudokuBoxArray = sudokuBoxes as ISudokuBoxBase[] ?? sudokuBoxes.ToArray();
            if (sudokuBoxArray.Count() != 9) throw new ArgumentOutOfRangeException(nameof(sudokuBoxes));
            mSudokuBoxes = sudokuBoxArray;

            InitializeSudokuBoxViewModels();
        }

        private void InitializeSudokuBoxViewModels()
        {
            mSudokuBoxViewModels.Clear();
            
            foreach (var sudokuBox in mSudokuBoxes)
            {
                switch (sudokuBox)
                {
                    case INoteSudokuBox noteSudokuBox:
                        mSudokuBoxViewModels.Add(new NoteSudokuBoxViewModel(noteSudokuBox));
                        break;
                    case IPredefinedSudokuBox predefinedSudokuBox:
                        mSudokuBoxViewModels.Add(new PredefinedSudokuBoxViewModel(predefinedSudokuBox));
                        break;
                    case IUserFilledSudokuBox userFilledSudokuBox:
                        mSudokuBoxViewModels.Add(new UserFilledSudokuBoxViewModel(userFilledSudokuBox));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(sudokuBox));
                }
            }
        }

        public BaseSudokuBoxViewModel FirstViewBoxModel => mSudokuBoxViewModels[0];
        public BaseSudokuBoxViewModel SecondViewBoxModel => mSudokuBoxViewModels[1];
        public BaseSudokuBoxViewModel ThirdViewBoxModel => mSudokuBoxViewModels[2];
        public BaseSudokuBoxViewModel FourthViewBoxModel => mSudokuBoxViewModels[3];
        public BaseSudokuBoxViewModel FifthViewBoxModel => mSudokuBoxViewModels[4];
        public BaseSudokuBoxViewModel SixthViewBoxModel => mSudokuBoxViewModels[5];
        public BaseSudokuBoxViewModel SeventhViewBoxModel => mSudokuBoxViewModels[6];
        public BaseSudokuBoxViewModel EighthViewBoxModel => mSudokuBoxViewModels[7];
        public BaseSudokuBoxViewModel NinthViewBoxModel => mSudokuBoxViewModels[8];

        protected override async Task InitializeAsync()
        {
            await base.InitializeAsync();
        }

        protected override async Task CloseAsync()
        {
            await base.CloseAsync();
        }
    }
}
