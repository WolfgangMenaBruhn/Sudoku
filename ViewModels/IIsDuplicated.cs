namespace Sudoku.ViewModels
{
    public interface IIsDuplicated
    {
        bool IsDirectDuplicated { get; set; }

        bool IsIndirectDuplicated { get; set; }

        bool IsDuplicated { get; }

        string Number { get; }
    }
}