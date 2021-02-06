using System.Collections.Generic;
using Sudoku.Contracts.Models;
using Sudoku.Contracts.Services;
using Sudoku.Models;
using Sudoku.ViewModels;

namespace Sudoku.Services
{
    public class SudokuService : ISudokuService
    {
        private readonly SudokuBoxCoordinate mMaxCoordinate = SudokuBoxCoordinate.GetMaxCoordinate3x3();
        private SudokuBoxCoordinate mCurrentParentCoordinate;
        private SudokuBoxCoordinate mCurrentChildCoordinate;

        #region delegates and events

        public delegate void ChangeUserDefinedToPredefinedNumberRequestedDelegate(
            IUserFilledSudokuBox userFilledSudokuBox, 
            SudokuBoxNumbers number);

        public event ChangeUserDefinedToPredefinedNumberRequestedDelegate ChangeUserDefinedToPredefinedNumberRequest;

        public delegate void ChangeUserDefinedToNotesDelegate(
            IUserFilledSudokuBox userDefinedBox);

        public event ChangeUserDefinedToNotesDelegate ChangeUserDefinedToNotesRequested;

        public delegate void ChangePredefinedToPredefinedNumberRequestedDelegate(
            IPredefinedSudokuBox preDefinedSudokuBox,
            SudokuBoxNumbers number);

        public event ChangePredefinedToPredefinedNumberRequestedDelegate ChangePredefinedToPredefinedNumberRequest;

        public delegate void DeletePredefinedNumberDelegate(IUserFilledSudokuBox predefinedSudokuBox);

        public event DeletePredefinedNumberDelegate DeletePredefinedNumberRequest;

        public delegate void ResetDelegate();

        public event ResetDelegate ResetRequest;

        public delegate void InformAboutClickedSudokuBoxDelegate(
            SudokuBoxBase clickedSudokuBox);

        public event InformAboutClickedSudokuBoxDelegate InformAboutClickedSudokuBox;

        public delegate void MarkDuplicatedNumbersDelegate();

        public event MarkDuplicatedNumbersDelegate MarkDuplicatedNumbersRequested;

        public delegate void CheckForFinishedDelegate();

        public event CheckForFinishedDelegate CheckForFinishedRequested;

        public delegate void CreateNoteBoxesDelegate();

        public event CreateNoteBoxesDelegate CreateNoteBoxesRequested;

        public delegate void ChangeNotesToUserDefinedDelegate(INoteSudokuBox notesBox);

        public event ChangeNotesToUserDefinedDelegate ChangeNotesToUserDefinedRequest;

        public delegate IEnumerable<SudokuBoxNumbers> ExistentNumbersDelegate(
            (SudokuBoxCoordinate parentCoordinate, SudokuBoxCoordinate coordinate) gridCoordinate);

        public event ExistentNumbersDelegate ExistentNumbersRequested;

        public delegate void ModeChangedDelegate(ControlSudokuMode mode);

        public event ModeChangedDelegate ModeChanged;

        #endregion

        public SudokuService()
        {
            Reset();
        }

        public IEnumerable<SudokuBoxNumbers> GetExistentNumbers(
            (SudokuBoxCoordinate parentCoordinate, SudokuBoxCoordinate coordinate) gridCoordinate)
        {
            return ExistentNumbersRequested?.Invoke(gridCoordinate);
        }
        
        public void CreateNoteBoxes()
        {
            CreateNoteBoxesRequested?.Invoke();
        }
        
        public void CheckForFinished()
        {
            CheckForFinishedRequested?.Invoke();
        }

        public void ConsiderControlPressedKey(SudokuBoxNumbers? number)
        {
            try
            {
                var userFilledBox = new UserFilledSudokuBox(
                    mCurrentChildCoordinate,
                    mCurrentParentCoordinate,
                    null);

                if (number != null)
                {
                    ChangeUserDefinedToPredefinedNumberRequest?.Invoke(
                        userFilledBox,
                        number.Value);

                    var predefinedBox = new PredefinedSudokuBox(
                        mCurrentChildCoordinate,
                        mCurrentParentCoordinate,
                        number.Value);

                    ChangePredefinedToPredefinedNumberRequest?.Invoke(
                        predefinedBox,
                        number.Value);
                }
                else
                {
                    DeletePredefinedNumberRequest?.Invoke(userFilledBox);
                }

                InformAboutClickedSudokuBox?.Invoke(userFilledBox);
                MarkDuplicatedNumbersRequested?.Invoke();
            }
            finally
            {
                var nextChildCoordinate = mCurrentChildCoordinate.GetNext(mMaxCoordinate);
                mCurrentChildCoordinate = nextChildCoordinate ??
                                          new SudokuBoxCoordinate(SudokuBoxNumbers.One, SudokuBoxNumbers.One);

                if (nextChildCoordinate == null)
                {
                    var nextParentCoordinate = mCurrentParentCoordinate.GetNext(mMaxCoordinate);
                    mCurrentParentCoordinate = nextParentCoordinate ??
                                               new SudokuBoxCoordinate(SudokuBoxNumbers.One, SudokuBoxNumbers.One);
                }
            }
        }

        public void SetMode(ControlSudokuMode mode)
        {
            Mode = mode;
            ModeChanged?.Invoke(mode);
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
            {
                ChangeUserDefinedToPredefinedNumberRequest?.Invoke(
                    userFilledSudokuBox,
                    PredefinedNumber.Value);
                MarkDuplicatedNumbersRequested?.Invoke();
            }
        }

        public void ChangeUserDefinedNumberToNotes(
            IUserFilledSudokuBox userFilledSudokuBox)
        {
            if (userFilledSudokuBox == null) return;

            ChangeUserDefinedToNotesRequested?.Invoke(userFilledSudokuBox);
        }

        public void ChangeNotesToUserDefined(
            INoteSudokuBox notesBox)
        {
            if (notesBox == null) return;
            ChangeNotesToUserDefinedRequest?.Invoke(notesBox);
        }

        public bool IsAllowedChangingUserDefinedNumberToPredefinedNumber() 
            => PredefinedNumber.HasValue && Mode.HasValue && Mode.Value == ControlSudokuMode.PreDefining;

        public bool IsAllowedChangingUserDefinedNumberToNotes()
            => Mode.HasValue && Mode.Value == ControlSudokuMode.Notes;

        public bool IsAllowedChangingNotesToUserDefined()
            => Mode.HasValue && Mode.Value == ControlSudokuMode.UserDefining;

        public bool IsAllowedSettingUserDefinedNumber()
            => PredefinedNumber.HasValue && Mode.HasValue && Mode.Value == ControlSudokuMode.UserDefining;

        public void ChangePredefinedNumberToPredefinedNumber(
            IPredefinedSudokuBox predefinedSudokuBox)
        {
            if (predefinedSudokuBox != null && IsAllowedChangingUserDefinedNumberToPredefinedNumber())
            {
                ChangePredefinedToPredefinedNumberRequest?.Invoke(
                    predefinedSudokuBox,
                    // ReSharper disable once PossibleInvalidOperationException
                    PredefinedNumber.Value);
                MarkDuplicatedNumbersRequested?.Invoke();
            }
        }

        public void NewGameRequested()
        {
            ResetRequest?.Invoke();
            MarkDuplicatedNumbersRequested?.Invoke();
            Reset();
        }

        private void Reset()
        {
            mCurrentParentCoordinate = new SudokuBoxCoordinate(SudokuBoxNumbers.One, SudokuBoxNumbers.One);
            mCurrentChildCoordinate = new SudokuBoxCoordinate(SudokuBoxNumbers.One, SudokuBoxNumbers.One);
        }

        public void SudokuBoxWasClicked(SudokuBoxBase clickedSudokuBox)
        {
            if ((clickedSudokuBox is IPredefinedSudokuBox predefinedBox && !predefinedBox.IsForControl ) ||
                clickedSudokuBox is IUserFilledSudokuBox)
            {
                mCurrentChildCoordinate = clickedSudokuBox.Coordinate;
                // ReSharper disable once PossibleInvalidOperationException
                mCurrentParentCoordinate = clickedSudokuBox.ParentCoordinate.Value;
            }

            InformAboutClickedSudokuBox?.Invoke(clickedSudokuBox);
            MarkDuplicatedNumbersRequested?.Invoke();
        }
    }
}