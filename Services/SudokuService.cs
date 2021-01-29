using Sudoku.Contracts.Models;
using Sudoku.Contracts.Services;
using Sudoku.Models;
using Sudoku.ViewModels;

namespace Sudoku.Services
{
    public class SudokuService : ISudokuService
    {
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

        public void SetMode(ControlSudokuMode mode)
        {
            if (Mode.HasValue && Mode.Value == ControlSudokuMode.PreDefining && mode != ControlSudokuMode.PreDefining)
                PredefinedNumber = null;

            Mode = mode;
        }

        public ControlSudokuMode? Mode { get; protected set; }

        public void SetPredefinedNumber(SudokuBoxNumbers predefinedNumber)
        {
            PredefinedNumber = predefinedNumber;
        }

        private SudokuBoxNumbers? mPredefinedNumber;

        public SudokuBoxNumbers? PredefinedNumber
        {
            get => Mode.HasValue && Mode.Value == ControlSudokuMode.PreDefining ? mPredefinedNumber : null;
            protected set => mPredefinedNumber = value;
        }

        public bool IsAllowedSettingPredefinedNumber(bool isControlContext)
            => isControlContext && Mode.HasValue && Mode.Value == ControlSudokuMode.PreDefining;

        public bool IsAllowedChangingPredefinedNumber(bool isControlContext)
            => !isControlContext && Mode.HasValue && Mode.Value == ControlSudokuMode.PreDefining;

        public void ChangeUserDefinedNumberToPredefinedNumber(
            IUserFilledSudokuBox userFilledSudokuBox)
        {
            if (userFilledSudokuBox != null && PredefinedNumber.HasValue)
                ChangeUserDefinedToPredefinedNumberRequest?.Invoke(
                    userFilledSudokuBox,
                    PredefinedNumber.Value);
        }

        public void ChangePredefinedNumberToPredefinedNumber(
            IPredefinedSudokuBox predefinedSudokuBox)
        {
            if (predefinedSudokuBox != null && PredefinedNumber.HasValue)
                ChangePredefinedToPredefinedNumberRequest?.Invoke(
                    predefinedSudokuBox,
                    PredefinedNumber.Value);
        }

        public void NewGameRequested()
        {
            ResetRequest?.Invoke();
        }
    }
}