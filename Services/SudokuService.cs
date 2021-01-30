using Sudoku.Contracts.Models;
using Sudoku.Contracts.Services;
using Sudoku.Models;
using Sudoku.ViewModels;

namespace Sudoku.Services
{
    public class SudokuService : ISudokuService
    {
        #region delegates and events

        public delegate void ChangeUserDefinedToPredefinedNumberRequestedDelegate(
            IUserFilledSudokuBox userFilledSudokuBox, 
            SudokuBoxNumbers number);

        public event ChangeUserDefinedToPredefinedNumberRequestedDelegate ChangeUserDefinedToPredefinedNumberRequest;

        public delegate void ChangePredefinedToPredefinedNumberRequestedDelegate(
            IPredefinedSudokuBox userFilledSudokuBox,
            SudokuBoxNumbers number);

        public event ChangePredefinedToPredefinedNumberRequestedDelegate ChangePredefinedToPredefinedNumberRequest;

        public delegate void ResetDelegate();

        public event ResetDelegate ResetRequest;

        public delegate void InformAboutClickedSudokuBoxDelegate(
            SudokuBoxBase clickedSudokuBox);

        public event InformAboutClickedSudokuBoxDelegate InformAboutClickedSudokuBox;

        #endregion

        public void SetMode(ControlSudokuMode mode)
        {
            Mode = mode;
        }

        public ControlSudokuMode? Mode { get; protected set; }

        public void SetPredefinedNumber(SudokuBoxNumbers predefinedNumber)
        {
            PredefinedNumber = predefinedNumber;
        }

        public SudokuBoxNumbers? PredefinedNumber { get; protected set; }

        public bool IsAllowedSettingPredefinedNumber(bool isControlContext) => isControlContext;

        public bool IsAllowedChangingPredefinedNumber(bool isControlContext)
            => !isControlContext && Mode.HasValue && Mode.Value == ControlSudokuMode.PreDefining;

        public bool IsAllowedSettingUserDefinedNumber(bool isControlContext)
            => !isControlContext && Mode.HasValue && Mode.Value == ControlSudokuMode.UserDefining;

        public void ChangeUserDefinedNumberToPredefinedNumber(
            IUserFilledSudokuBox userFilledSudokuBox)
        {
            if (userFilledSudokuBox != null && PredefinedNumber.HasValue)
                ChangeUserDefinedToPredefinedNumberRequest?.Invoke(
                    userFilledSudokuBox,
                    PredefinedNumber.Value);
        }

        public bool IsAllowedChangingUserDefinedNumberToPredefinedNumber() 
            => PredefinedNumber.HasValue && Mode.HasValue && Mode.Value == ControlSudokuMode.PreDefining;

        public bool IsAllowedSettingUserDefinedNumber()
            => PredefinedNumber.HasValue && Mode.HasValue && Mode.Value == ControlSudokuMode.UserDefining;

        public void ChangePredefinedNumberToPredefinedNumber(
            IPredefinedSudokuBox predefinedSudokuBox)
        {
            if (predefinedSudokuBox != null && IsAllowedChangingUserDefinedNumberToPredefinedNumber())
                ChangePredefinedToPredefinedNumberRequest?.Invoke(
                    predefinedSudokuBox,
                    // ReSharper disable once PossibleInvalidOperationException
                    PredefinedNumber.Value);
        }

        public void NewGameRequested()
        {
            ResetRequest?.Invoke();
        }

        public void SudokuBoxWasClicked(SudokuBoxBase clickedSudokuBox)
        {
            InformAboutClickedSudokuBox?.Invoke(clickedSudokuBox);
        }
    }
}