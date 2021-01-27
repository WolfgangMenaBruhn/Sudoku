namespace Sudoku.Models
{
    public enum SudokuBoxState
    {
        /// <summary>
        /// The given number is predefined
        /// </summary>
        Predefined,
        /// <summary>
        /// The field is used for notes
        /// </summary>
        Note,
        /// <summary>
        /// The given number was defined by the user
        /// </summary>
        UserFilled
    }
}