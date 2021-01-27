using System.Collections.Generic;
using Catel.MVVM;
using System.Threading.Tasks;
using Sudoku.Models;

namespace Sudoku.ViewModels
{
    public class ControlSudokuViewModel : ViewModelBase
    {
        private readonly List<PredefinedSudokuBoxViewModel> mPredefinedSudokuBoxes = new List<PredefinedSudokuBoxViewModel>();
        
        public ControlSudokuViewModel()
        {
            InitializePredefinedSudokuBoxes();
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
            RefreshValues();
        }

        #endregion

        #region mode

        private ControlSudokuMode mControlSudokuMode = ControlSudokuMode.PreDefining;
        
        // ReSharper disable once ConvertToAutoProperty
        public ControlSudokuMode ControlSudokuMode
        {
            get => mControlSudokuMode;
            set => mControlSudokuMode = value;
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

        private void InitializePredefinedSudokuBoxes()
        {
            mPredefinedSudokuBoxes.Add(
                new PredefinedSudokuBoxViewModel(
                        new PredefinedSudokuBox(
                            new SudokuBoxCoordinate(
                                SudokuBoxNumbers.One, 
                                SudokuBoxNumbers.One),
                    SudokuBoxNumbers.One)));

            mPredefinedSudokuBoxes.Add(
                new PredefinedSudokuBoxViewModel(
                    new PredefinedSudokuBox(
                        new SudokuBoxCoordinate(
                            SudokuBoxNumbers.One,
                            SudokuBoxNumbers.Two),
                        SudokuBoxNumbers.Two)));

            mPredefinedSudokuBoxes.Add(
                new PredefinedSudokuBoxViewModel(
                    new PredefinedSudokuBox(
                        new SudokuBoxCoordinate(
                            SudokuBoxNumbers.One,
                            SudokuBoxNumbers.Three),
                        SudokuBoxNumbers.Three)));

            mPredefinedSudokuBoxes.Add(
                new PredefinedSudokuBoxViewModel(
                    new PredefinedSudokuBox(
                        new SudokuBoxCoordinate(
                            SudokuBoxNumbers.Two,
                            SudokuBoxNumbers.One),
                        SudokuBoxNumbers.Four)));

            mPredefinedSudokuBoxes.Add(
                new PredefinedSudokuBoxViewModel(
                    new PredefinedSudokuBox(
                        new SudokuBoxCoordinate(
                            SudokuBoxNumbers.Two,
                            SudokuBoxNumbers.Two),
                        SudokuBoxNumbers.Five)));

            mPredefinedSudokuBoxes.Add(
                new PredefinedSudokuBoxViewModel(
                    new PredefinedSudokuBox(
                        new SudokuBoxCoordinate(
                            SudokuBoxNumbers.Two,
                            SudokuBoxNumbers.Three),
                        SudokuBoxNumbers.Six)));

            mPredefinedSudokuBoxes.Add(
                new PredefinedSudokuBoxViewModel(
                    new PredefinedSudokuBox(
                        new SudokuBoxCoordinate(
                            SudokuBoxNumbers.Three,
                            SudokuBoxNumbers.One),
                        SudokuBoxNumbers.Seven)));

            mPredefinedSudokuBoxes.Add(
                new PredefinedSudokuBoxViewModel(
                    new PredefinedSudokuBox(
                        new SudokuBoxCoordinate(
                            SudokuBoxNumbers.Three,
                            SudokuBoxNumbers.Two),
                        SudokuBoxNumbers.Eight)));

            mPredefinedSudokuBoxes.Add(
                new PredefinedSudokuBoxViewModel(
                    new PredefinedSudokuBox(
                        new SudokuBoxCoordinate(
                            SudokuBoxNumbers.Three,
                            SudokuBoxNumbers.Three),
                        SudokuBoxNumbers.Nine)));
        }

        public PredefinedSudokuBoxViewModel Box1 => mPredefinedSudokuBoxes[0];
        public PredefinedSudokuBoxViewModel Box2 => mPredefinedSudokuBoxes[1];
        public PredefinedSudokuBoxViewModel Box3 => mPredefinedSudokuBoxes[2];
        public PredefinedSudokuBoxViewModel Box4 => mPredefinedSudokuBoxes[3];
        public PredefinedSudokuBoxViewModel Box5 => mPredefinedSudokuBoxes[4];
        public PredefinedSudokuBoxViewModel Box6 => mPredefinedSudokuBoxes[5];
        public PredefinedSudokuBoxViewModel Box7 => mPredefinedSudokuBoxes[6];
        public PredefinedSudokuBoxViewModel Box8 => mPredefinedSudokuBoxes[7];
        public PredefinedSudokuBoxViewModel Box9 => mPredefinedSudokuBoxes[8];

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
