using System.Collections.Generic;
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

        bool IsAllowedChangingUserDefinedNumberToNotes();

        bool IsAllowedChangingNotesToUserDefined();

        bool IsAllowedSettingUserDefinedNumber();

        void ChangeUserDefinedNumberToPredefinedNumber(
            IUserFilledSudokuBox userFilledSudokuBox);

        void ChangeUserDefinedNumberToNotes(
            IUserFilledSudokuBox userFilledSudokuBox);

        void ChangeNotesToUserDefined(
            INoteSudokuBox notesBox);

        event SudokuService.ChangeUserDefinedToPredefinedNumberRequestedDelegate
            ChangeUserDefinedToPredefinedNumberRequest;

        event SudokuService.ChangeUserDefinedToNotesDelegate ChangeUserDefinedToNotesRequested;

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

        void ConsiderControlPressedKey(SudokuBoxNumbers? number);

        event SudokuService.DeletePredefinedNumberDelegate DeletePredefinedNumberRequest;

        event SudokuService.MarkDuplicatedNumbersDelegate MarkDuplicatedNumbersRequested;

        event SudokuService.CheckForFinishedDelegate CheckForFinishedRequested;

        void CheckForFinished();

        event SudokuService.CreateNoteBoxesDelegate CreateNoteBoxesRequested;

        void CreateNoteBoxes();

        event SudokuService.ChangeNotesToUserDefinedDelegate ChangeNotesToUserDefinedRequest;

        IEnumerable<SudokuBoxNumbers> GetExistentNumbers(
            (SudokuBoxCoordinate parentCoordinate, SudokuBoxCoordinate coordinate) gridCoordinate);

        event SudokuService.ExistentNumbersDelegate ExistentNumbersRequested;

        event SudokuService.ModeChangedDelegate ModeChanged;
    }
}