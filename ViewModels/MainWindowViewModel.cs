using Catel.MVVM;
using System.Threading.Tasks;

namespace Sudoku.ViewModels
{
    // ReSharper disable once UnusedMember.Global
    public class MainWindowViewModel : ViewModelBase
    {
        // ReSharper disable once EmptyConstructor
        public MainWindowViewModel()
        {
        }

        public override string Title => "Welcome to Sudoku";

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
