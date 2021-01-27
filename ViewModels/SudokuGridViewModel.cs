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
        private readonly List<BaseSudokuBoxViewModel> mSudokuBoxViewModels = new List<BaseSudokuBoxViewModel>(81);
        private readonly IEnumerable<ISudokuBoxBase> mSudokuBoxes;

        public SudokuGridViewModel(IEnumerable<ISudokuBoxBase> sudokuBoxes)
        {
            if (sudokuBoxes == null) throw new ArgumentNullException(nameof(sudokuBoxes));
            var sudokuBoxArray = sudokuBoxes as ISudokuBoxBase[] ?? sudokuBoxes.ToArray();
            if (sudokuBoxArray.Count() != 81) throw new ArgumentOutOfRangeException(nameof(sudokuBoxes));
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

        #region view box models

        public BaseSudokuBoxViewModel ViewBoxModel_1_1 => mSudokuBoxViewModels[0];
        public BaseSudokuBoxViewModel ViewBoxModel_1_2 => mSudokuBoxViewModels[1];
        public BaseSudokuBoxViewModel ViewBoxModel_1_3 => mSudokuBoxViewModels[2];
        public BaseSudokuBoxViewModel ViewBoxModel_1_4 => mSudokuBoxViewModels[3];
        public BaseSudokuBoxViewModel ViewBoxModel_1_5 => mSudokuBoxViewModels[4];
        public BaseSudokuBoxViewModel ViewBoxModel_1_6 => mSudokuBoxViewModels[5];
        public BaseSudokuBoxViewModel ViewBoxModel_1_7 => mSudokuBoxViewModels[6];
        public BaseSudokuBoxViewModel ViewBoxModel_1_8 => mSudokuBoxViewModels[7];
        public BaseSudokuBoxViewModel ViewBoxModel_1_9 => mSudokuBoxViewModels[8];

        public BaseSudokuBoxViewModel ViewBoxModel_2_1 => mSudokuBoxViewModels[9];
        public BaseSudokuBoxViewModel ViewBoxModel_2_2 => mSudokuBoxViewModels[10];
        public BaseSudokuBoxViewModel ViewBoxModel_2_3 => mSudokuBoxViewModels[11];
        public BaseSudokuBoxViewModel ViewBoxModel_2_4 => mSudokuBoxViewModels[12];
        public BaseSudokuBoxViewModel ViewBoxModel_2_5 => mSudokuBoxViewModels[13];
        public BaseSudokuBoxViewModel ViewBoxModel_2_6 => mSudokuBoxViewModels[14];
        public BaseSudokuBoxViewModel ViewBoxModel_2_7 => mSudokuBoxViewModels[15];
        public BaseSudokuBoxViewModel ViewBoxModel_2_8 => mSudokuBoxViewModels[16];
        public BaseSudokuBoxViewModel ViewBoxModel_2_9 => mSudokuBoxViewModels[17];

        public BaseSudokuBoxViewModel ViewBoxModel_3_1 => mSudokuBoxViewModels[18];
        public BaseSudokuBoxViewModel ViewBoxModel_3_2 => mSudokuBoxViewModels[19];
        public BaseSudokuBoxViewModel ViewBoxModel_3_3 => mSudokuBoxViewModels[20];
        public BaseSudokuBoxViewModel ViewBoxModel_3_4 => mSudokuBoxViewModels[21];
        public BaseSudokuBoxViewModel ViewBoxModel_3_5 => mSudokuBoxViewModels[22];
        public BaseSudokuBoxViewModel ViewBoxModel_3_6 => mSudokuBoxViewModels[23];
        public BaseSudokuBoxViewModel ViewBoxModel_3_7 => mSudokuBoxViewModels[24];
        public BaseSudokuBoxViewModel ViewBoxModel_3_8 => mSudokuBoxViewModels[25];
        public BaseSudokuBoxViewModel ViewBoxModel_3_9 => mSudokuBoxViewModels[26];

        public BaseSudokuBoxViewModel ViewBoxModel_4_1 => mSudokuBoxViewModels[27];
        public BaseSudokuBoxViewModel ViewBoxModel_4_2 => mSudokuBoxViewModels[28];
        public BaseSudokuBoxViewModel ViewBoxModel_4_3 => mSudokuBoxViewModels[29];
        public BaseSudokuBoxViewModel ViewBoxModel_4_4 => mSudokuBoxViewModels[30];
        public BaseSudokuBoxViewModel ViewBoxModel_4_5 => mSudokuBoxViewModels[31];
        public BaseSudokuBoxViewModel ViewBoxModel_4_6 => mSudokuBoxViewModels[32];
        public BaseSudokuBoxViewModel ViewBoxModel_4_7 => mSudokuBoxViewModels[33];
        public BaseSudokuBoxViewModel ViewBoxModel_4_8 => mSudokuBoxViewModels[34];
        public BaseSudokuBoxViewModel ViewBoxModel_4_9 => mSudokuBoxViewModels[35];

        public BaseSudokuBoxViewModel ViewBoxModel_5_1 => mSudokuBoxViewModels[36];
        public BaseSudokuBoxViewModel ViewBoxModel_5_2 => mSudokuBoxViewModels[37];
        public BaseSudokuBoxViewModel ViewBoxModel_5_3 => mSudokuBoxViewModels[38];
        public BaseSudokuBoxViewModel ViewBoxModel_5_4 => mSudokuBoxViewModels[39];
        public BaseSudokuBoxViewModel ViewBoxModel_5_5 => mSudokuBoxViewModels[40];
        public BaseSudokuBoxViewModel ViewBoxModel_5_6 => mSudokuBoxViewModels[41];
        public BaseSudokuBoxViewModel ViewBoxModel_5_7 => mSudokuBoxViewModels[42];
        public BaseSudokuBoxViewModel ViewBoxModel_5_8 => mSudokuBoxViewModels[43];
        public BaseSudokuBoxViewModel ViewBoxModel_5_9 => mSudokuBoxViewModels[44];

        public BaseSudokuBoxViewModel ViewBoxModel_6_1 => mSudokuBoxViewModels[45];
        public BaseSudokuBoxViewModel ViewBoxModel_6_2 => mSudokuBoxViewModels[46];
        public BaseSudokuBoxViewModel ViewBoxModel_6_3 => mSudokuBoxViewModels[47];
        public BaseSudokuBoxViewModel ViewBoxModel_6_4 => mSudokuBoxViewModels[48];
        public BaseSudokuBoxViewModel ViewBoxModel_6_5 => mSudokuBoxViewModels[49];
        public BaseSudokuBoxViewModel ViewBoxModel_6_6 => mSudokuBoxViewModels[50];
        public BaseSudokuBoxViewModel ViewBoxModel_6_7 => mSudokuBoxViewModels[51];
        public BaseSudokuBoxViewModel ViewBoxModel_6_8 => mSudokuBoxViewModels[52];
        public BaseSudokuBoxViewModel ViewBoxModel_6_9 => mSudokuBoxViewModels[53];

        public BaseSudokuBoxViewModel ViewBoxModel_7_1 => mSudokuBoxViewModels[54];
        public BaseSudokuBoxViewModel ViewBoxModel_7_2 => mSudokuBoxViewModels[55];
        public BaseSudokuBoxViewModel ViewBoxModel_7_3 => mSudokuBoxViewModels[56];
        public BaseSudokuBoxViewModel ViewBoxModel_7_4 => mSudokuBoxViewModels[57];
        public BaseSudokuBoxViewModel ViewBoxModel_7_5 => mSudokuBoxViewModels[58];
        public BaseSudokuBoxViewModel ViewBoxModel_7_6 => mSudokuBoxViewModels[59];
        public BaseSudokuBoxViewModel ViewBoxModel_7_7 => mSudokuBoxViewModels[60];
        public BaseSudokuBoxViewModel ViewBoxModel_7_8 => mSudokuBoxViewModels[61];
        public BaseSudokuBoxViewModel ViewBoxModel_7_9 => mSudokuBoxViewModels[62];

        public BaseSudokuBoxViewModel ViewBoxModel_8_1 => mSudokuBoxViewModels[63];
        public BaseSudokuBoxViewModel ViewBoxModel_8_2 => mSudokuBoxViewModels[64];
        public BaseSudokuBoxViewModel ViewBoxModel_8_3 => mSudokuBoxViewModels[65];
        public BaseSudokuBoxViewModel ViewBoxModel_8_4 => mSudokuBoxViewModels[66];
        public BaseSudokuBoxViewModel ViewBoxModel_8_5 => mSudokuBoxViewModels[67];
        public BaseSudokuBoxViewModel ViewBoxModel_8_6 => mSudokuBoxViewModels[68];
        public BaseSudokuBoxViewModel ViewBoxModel_8_7 => mSudokuBoxViewModels[69];
        public BaseSudokuBoxViewModel ViewBoxModel_8_8 => mSudokuBoxViewModels[70];
        public BaseSudokuBoxViewModel ViewBoxModel_8_9 => mSudokuBoxViewModels[71];

        public BaseSudokuBoxViewModel ViewBoxModel_9_1 => mSudokuBoxViewModels[72];
        public BaseSudokuBoxViewModel ViewBoxModel_9_2 => mSudokuBoxViewModels[73];
        public BaseSudokuBoxViewModel ViewBoxModel_9_3 => mSudokuBoxViewModels[74];
        public BaseSudokuBoxViewModel ViewBoxModel_9_4 => mSudokuBoxViewModels[75];
        public BaseSudokuBoxViewModel ViewBoxModel_9_5 => mSudokuBoxViewModels[76];
        public BaseSudokuBoxViewModel ViewBoxModel_9_6 => mSudokuBoxViewModels[77];
        public BaseSudokuBoxViewModel ViewBoxModel_9_7 => mSudokuBoxViewModels[78];
        public BaseSudokuBoxViewModel ViewBoxModel_9_8 => mSudokuBoxViewModels[79];
        public BaseSudokuBoxViewModel ViewBoxModel_9_9 => mSudokuBoxViewModels[80];
        #endregion

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
