﻿using Catel.MVVM;
using System.Threading.Tasks;
using Sudoku.Contracts.Models;

namespace Sudoku.ViewModels
{
    public abstract class BaseSudokuBoxViewModel : ViewModelBase
    {
        protected ISudokuBoxBase mModel;

        public bool IsSelected { get; set; }

        public bool IsHighlighted { get; set; }

        public bool IsSelectedOrHighlighted => IsSelected || IsHighlighted;

        protected void RefreshValues()
        {
            RaisePropertyChanged(nameof(IsSelected));
            RaisePropertyChanged(nameof(IsHighlighted));
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
