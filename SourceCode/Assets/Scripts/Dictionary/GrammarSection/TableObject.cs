using System.Collections.Generic;

namespace EnglishApp
{
    public class TableObject
    {
        private string title;
        public string Title
        {
            set { this.title = value; }
            get { return this.title; }
        }

        private int heightTable;
        public int HeightTable
        {
            set { this.heightTable = value; }
            get { return this.heightTable; }
        }

        private List<string> headerRow;
        public List<string> HeaderRow
        {
            set { this.headerRow = value; }
            get { return this.headerRow; }
        }

        private bool hasHeaderRow;
        public bool HasHeaderRow
        {
            set { this.hasHeaderRow = value; }
            get { return this.hasHeaderRow; }
        }

        private List<string> rows;
        public List<string> Rows
        {
            set { this.rows = value; }
            get { return this.rows; }
        }

        public TableObject() { }

        #region Table
        private int nRows;
        public int NRows
        {
            set { this.nRows = value; }
            get { return this.nRows; }
        }
        private int nCols;
        public int NCols
        {
            set { this.nCols = value; }
            get { return this.nCols; }
        }

        private string[,] table;
        public string[,] Table
        {
            set { this.table = value; }
            get { return this.table; }
        }

        /// <summary>
        /// Sets the table with the data
        /// </summary>
        public void SetupTable()
        {
            // nRows = rows + header row
            if (hasHeaderRow)
            {
                nRows = rows.Count + 1;
            }
            else
            {
                nRows = rows.Count;
            }
            nCols = headerRow.Count;
            table = new string[nRows, nCols];

            int startRow = 0;
            if (hasHeaderRow)
            {
                for (int iCol = 0; iCol < headerRow.Count; iCol++)
                {
                    table[startRow, iCol] = headerRow[iCol];
                }
                startRow = 1;
            }

            int indexRow = 0;
            for (int iRow = startRow; iRow < nRows; iRow++)
            {
                string[] auxCell = rows[indexRow].Split('|');
                indexRow++;
                if ((auxCell != null) && (auxCell.Length >= nCols))
                {
                    for (int iCol = 0; iCol < nCols; iCol++)
                    {
                        table[iRow, iCol] = auxCell[iCol];
                    }
                }
            }
        }
        #endregion Table

    }
}
