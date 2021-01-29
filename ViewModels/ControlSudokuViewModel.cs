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
            await base.CloseAsync();
        }
    }
}
