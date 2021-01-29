using Sudoku.Contracts.Models;
using Sudoku.Models;
using Sudoku.Services;
using Sudoku.ViewModels;

namespace Sudoku.Contracts.Services
{
    public interface ISudokuService
    {
        void SetMode(ControlSudokuMode mode);

        ControlSudokuMode? Mode { get; }

        void SetPredefinedNumber(
            SudokuBoxNumbers predefinedNumber);

        SudokuBoxNumbers? PredefinedNumber { get; }

        bool IsAllowedSettingPredefinedNumber(
            bool isControlContext);

        void ChangeUserDefinedNumberToPredefinedNumber(
            IUserFilledSudokuBox userFilledSudokuBox);

        event SudokuService.ChangeUserDefinedToPredefinedNumberRequestedDelegate
            ChangeUserDefinedToPredefinedNumberRequest;
    }
}