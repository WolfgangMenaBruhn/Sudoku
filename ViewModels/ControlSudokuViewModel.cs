using System.Collections.Generic;
using Catel.MVVM;
using System.Threading.Tasks;
using Sudoku.Contracts.Services;

namespace Sudoku.ViewModels
{
    public class ControlSudokuViewModel : ViewModelBase
    {
        private SudokuGridPartViewModel mControlGridPartViewModel;
        private readonly IModelsFactoryService mModelsFactoryService;
        private readonly ISudokuService mSudokuService;

        public ControlSudokuViewModel(
            IModelsFactoryService modelsFactoryService,
            ISudokuService sudokuService)
        {
            mModelsFactoryService = modelsFactoryService;
            mSudokuService = sudokuService;

            ControlSudokuMode = ControlSudokuMode.PreDefining;
            InitializeControlGrid();

            mSudokuService.ModeChanged += OnModeChanged;
        }

        private void OnModeChanged(ControlSudokuMode mode)
        {
            mControlSudokuMode = mode; // Intentionally using field
            RefreshValues();
        }

        #region new game command

        private Command mNewGameCommand;

        // ReSharper disable once UnusedMember.Global
        public Command NewGameCommand =>
            mNewGameCommand ??
            (mNewGameCommand =
                new Command(
                    ExecuteNewGameCommand));

        private void ExecuteNewGameCommand()
        {
            ControlSudokuMode = ControlSudokuMode.PreDefining;
            mSudokuService.NewGameRequested();
            RefreshValues();
        }

        #endregion

        #region create note boxes command

        private Command mCreateNoteBoxesCommand;

        // ReSharper disable once UnusedMember.Global
        public Command CreateNoteBoxesCommand =>
            mCreateNoteBoxesCommand ??
            (mCreateNoteBoxesCommand =
                new Command(
                    ExecuteCreateNoteBoxesCommand));

        private void ExecuteCreateNoteBoxesCommand()
        {
            mSudokuService.CreateNoteBoxes();
        }

        #endregion

        #region Key 0 command

        private Command mKey0Command;

        // ReSharper disable once UnusedMember.Global
        public Command Key0Command =>
            mKey0Command ??
            (mKey0Command =
                new Command(
                    ExecuteKey0Command));

        private void ExecuteKey0Command()
        {
            mSudokuService.ConsiderSudokuControlPressedKey(null);
        }

        #endregion

        #region Key 1 command

        private Command mKey1Command;

        // ReSharper disable once UnusedMember.Global
        public Command Key1Command =>
            mKey1Command ??
            (mKey1Command =
                new Command(
                    ExecuteKey1Command));

        private void ExecuteKey1Command()
        {
            mSudokuService.ConsiderSudokuControlPressedKey(Models.SudokuBoxNumbers.One);
        }

        #endregion

        #region Key 2 command

        private Command mKey2Command;

        // ReSharper disable once UnusedMember.Global
        public Command Key2Command =>
            mKey2Command ??
            (mKey2Command =
                new Command(
                    ExecuteKey2Command));

        private void ExecuteKey2Command()
        {
            mSudokuService.ConsiderSudokuControlPressedKey(Models.SudokuBoxNumbers.Two);
        }

        #endregion

        #region Key 3 command

        private Command mKey3Command;

        // ReSharper disable once UnusedMember.Global
        public Command Key3Command =>
            mKey3Command ??
            (mKey3Command =
                new Command(
                    ExecuteKey3Command));

        private void ExecuteKey3Command()
        {
            mSudokuService.ConsiderSudokuControlPressedKey(Models.SudokuBoxNumbers.Three);
        }

        #endregion

        #region Key 4 command

        private Command mKey4Command;

        // ReSharper disable once UnusedMember.Global
        public Command Key4Command =>
            mKey4Command ??
            (mKey4Command =
                new Command(
                    ExecuteKey4Command));

        private void ExecuteKey4Command()
        {
            mSudokuService.ConsiderSudokuControlPressedKey(Models.SudokuBoxNumbers.Four);
        }

        #endregion

        #region Key 5 command

        private Command mKey5Command;

        // ReSharper disable once UnusedMember.Global
        public Command Key5Command =>
            mKey5Command ??
            (mKey5Command =
                new Command(
                    ExecuteKey5Command));

        private void ExecuteKey5Command()
        {
            mSudokuService.ConsiderSudokuControlPressedKey(Models.SudokuBoxNumbers.Five);
        }

        #endregion

        #region Key 6 command

        private Command mKey6Command;

        // ReSharper disable once UnusedMember.Global
        public Command Key6Command =>
            mKey6Command ??
            (mKey6Command =
                new Command(
                    ExecuteKey6Command));

        private void ExecuteKey6Command()
        {
            mSudokuService.ConsiderSudokuControlPressedKey(Models.SudokuBoxNumbers.Six);
        }

        #endregion

        #region Key 7 command

        private Command mKey7Command;

        // ReSharper disable once UnusedMember.Global
        public Command Key7Command =>
            mKey7Command ??
            (mKey7Command =
                new Command(
                    ExecuteKey7Command));

        private void ExecuteKey7Command()
        {
            mSudokuService.ConsiderSudokuControlPressedKey(Models.SudokuBoxNumbers.Seven);
        }

        #endregion

        #region Key 8 command

        private Command mKey8Command;

        // ReSharper disable once UnusedMember.Global
        public Command Key8Command =>
            mKey8Command ??
            (mKey8Command =
                new Command(
                    ExecuteKey8Command));

        private void ExecuteKey8Command()
        {
            mSudokuService.ConsiderSudokuControlPressedKey(Models.SudokuBoxNumbers.Eight);
        }

        #endregion

        #region Key 9 command

        private Command mKey9Command;

        // ReSharper disable once UnusedMember.Global
        public Command Key9Command =>
            mKey9Command ??
            (mKey9Command =
                new Command(
                    ExecuteKey9Command));

        private void ExecuteKey9Command()
        {
            mSudokuService.ConsiderSudokuControlPressedKey(Models.SudokuBoxNumbers.Nine);
        }

        #endregion

        #region mode

        private ControlSudokuMode mControlSudokuMode;
        
        // ReSharper disable once ConvertToAutoProperty
        public ControlSudokuMode ControlSudokuMode
        {
            get => mControlSudokuMode;
            set
            {
                mControlSudokuMode = value;
                mSudokuService.SetMode(value);
            }
        }

        public Dictionary<ControlSudokuMode, string> ControlSudokuModes { get; } =
            new Dictionary<ControlSudokuMode, string>
            {
                {ControlSudokuMode.PreDefining, "Define start values"},
                {ControlSudokuMode.UserDefining, "Set your own values"},
                {ControlSudokuMode.Notes, "Set your notes"}
            };

        #endregion

        #region predefined boxes

        private void InitializeControlGrid()
        {
            mControlGridPartViewModel =
                new SudokuGridPartViewModel(
                    mModelsFactoryService.GetNinePredefinedSudokuBoxes(),
                    mSudokuService,
                    mModelsFactoryService);
        }

        public SudokuGridPartViewModel ControlGrid => mControlGridPartViewModel;

        #endregion

        private void RefreshValues()
        {
            RaisePropertyChanged(nameof(ControlSudokuMode));
        }

        public override string Title => "View model title";

        protected override async Task InitializeAsync()
        {
            await base.InitializeAsync();
        }

        protected override async Task CloseAsync()
        {
            mSudokuService.ModeChanged -= OnModeChanged;
            await base.CloseAsync();
        }
    }
}
