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

        bool IsAllowedChangingUserDefinedNumberToPredefinedNumber();

        bool IsAllowedSettingUserDefinedNumber();

        void ChangeUserDefinedNumberToPredefinedNumber(
            IUserFilledSudokuBox userFilledSudokuBox);

        event SudokuService.ChangeUserDefinedToPredefinedNumberRequestedDelegate
            ChangeUserDefinedToPredefinedNumberRequest;

        void ChangePredefinedNumberToPredefinedNumber(
            IPredefinedSudokuBox predefinedSudokuBox);

        event SudokuService.ChangePredefinedToPredefinedNumberRequestedDelegate
            ChangePredefinedToPredefinedNumberRequest;

        bool IsAllowedChangingPredefinedNumber(
            bool isControlContext);

        bool IsAllowedSettingUserDefinedNumber(
            bool isControlContext);

        event SudokuService.ResetDelegate ResetRequest;

        void NewGameRequested();

        void SudokuBoxWasClicked(SudokuBoxBase clickedSudokuBox);

        event SudokuService.InformAboutClickedSudokuBoxDelegate InformAboutClickedSudokuBox;
    }
}