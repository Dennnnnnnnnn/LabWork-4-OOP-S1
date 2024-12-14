namespace LW4
{
    /// <summary>
    /// Class to store 2D object collection
    /// </summary>
    internal class Matrix
    {
        const int CMaxRows = 10; // Maximum number of rows
        const int CMaxColumns = 10; // Maximum number of columns
        private int[,] matrix; // 2D-array
        /// <summary>
        /// Getter and setter for number of rows
        /// </summary>
        public int numberOfRows { get; set; }
        /// <summary>
        /// Getter and setter for number of rows
        /// </summary>
        public int numberOfColumns { get; set; }
        /// <summary>
        /// Constructor without parameters (default)
        /// </summary>
        public Matrix()
        {
            numberOfRows = 0;
            numberOfColumns = 0;
            matrix = new int[CMaxRows, CMaxColumns];
        }
        /// <summary>
        /// Sets a value to a matrix cell
        /// </summary>
        /// <param name="row"> Matrix row</param>
        /// <param name="col"> Matrix column</param>
        /// <param name="value"> Value</param>
        public void SetValue(int row, int col, int value)
        {
            matrix[row, col] = value;
        }
        /// <summary>
        /// Gets a value from matrix cell
        /// </summary>
        /// <param name="row"> Matrix row</param>
        /// <param name="col"> Matrix column</param>
        /// <returns> Value from matrix cell</returns>
        public int GetValue(int row, int col)
        {
            return matrix[row, col];
        }
        /// <summary>
        /// Swaps two rows
        /// </summary>
        /// <param name="row1"> Row 1</param>
        /// <param name="row2"> Row 2</param>
        public void SwapRows(int row1, int row2)
        {
            for (int col = 0; col < numberOfColumns; col++)
            {
                int temp = GetValue(row2, col);
                SetValue(row2, col, GetValue(row1, col));
                SetValue(row1, col, temp);
            }
        }
        /// <summary>
        /// Swaps two columns
        /// </summary>
        /// <param name="col1"> Column 1</param>
        /// <param name="col2"> Column 2</param>
        public void SwapColumns(int col1, int col2)
        {
            for (int row = 0; row < numberOfRows; row++)
            {
                int temp = GetValue(row, col2);
                SetValue(row, col2, GetValue(row, col1));
                SetValue(row, col1, temp);
            }
        }
        /// <summary>
        /// Removes column
        /// </summary>
        /// <param name="ind"> Index of a column 
        /// that need to be removed</param>
        public void RemoveColumn(int ind)
        {

            for (int col = ind; col < numberOfColumns - 1; col++)
            {
                for (int row = 0; row < numberOfRows; row++)
                {
                    int value = GetValue(row, col + 1);
                    SetValue(row, col, value);
                }
            }
            numberOfColumns--;
        }
    }
}
