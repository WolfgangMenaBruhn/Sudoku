using Catel.MVVM;
using System.Threading.Tasks;
using Sudoku.Contracts.Models;

namespace Sudoku.ViewModels
{
    public abstract class BaseSudokuBoxViewModel : ViewModelBase
    {
        protected ISudokuBoxBase mModel;
        private bool mIsSelected;
        private bool mIsHighlighted
                ;

        private bool mIsSameNumber;

        public bool IsSelected
        {
            get => mIsSelected;
            set
            {
                mIsSelected = value;
                RefreshValues();
            }
        }

        public bool IsHighlighted
        {
            get => mIsHighlighted;
            set
            {
                mIsHighlighted = value;
                RefreshValues();
            }
        }

        public bool IsSelectedOrHighlighted => IsSelected || IsHighlighted;

        public bool IsSameNumber
        {
            get => mIsSameNumber;
            set
            {
                mIsSameNumber = value;
                RefreshValues();
            }
        }

        protected void RefreshValues()
        {
            RaisePropertyChanged(nameof(IsSelected));
            RaisePropertyChanged(nameof(IsHighlighted));
            RaisePropertyChanged(nameof(IsSameNumber));
            RaisePropertyChanged(nameof(IsSelectedOrHighlighted));
        }

        public ISudokuBoxBase GetModel() => mModel;

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
