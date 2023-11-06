using A3.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A3.DAO
{
    public class SBMDAO : IDAO
    {
        Sudoku sbm;
        private string fileName;

        public SBMDAO(string fileName)
        {
            sbm = new Sudoku(fileName);
            this.fileName = fileName;
        }

        public bool Contains1To9(int[] data)
        {
            int[] checkNums = new int[9];
            bool correct = true;
            int i = 0;

            while (i < data.Length && correct)
            {
                if (data[i] < 1 && data[i] > 9 || checkNums.Contains(data[i]))
                {
                    correct = false;
                }
                checkNums[i] = (data[i]);
                i++;
            }
            return correct;
        }

        public int[] ReadRow(int rowNumber)
        {
            int[] row = new int[9];
            int rowValue;

            if (rowNumber < 1 || rowNumber > 9) { throw new Exception("Invalid row"); }

            int startPosition = (rowNumber - 1) * 9 * sizeof(int);
            sbm.FS.Seek(startPosition, SeekOrigin.Begin);

            for (int i = 0; i < 9; i++)
            {
                rowValue = sbm.BR.ReadInt32();
                row[i] = rowValue;
            }
            return row;
        }

        public bool RowCorrect(int row)
        {
            bool correct = false;
            int[] checkRow = ReadRow(row);

            if (Contains1To9(checkRow)) { correct = true; }

            return correct;
        }

        public int[] ReadColumn(int colNumber)
        {
            int[] col = new int[9];
            int colValue;

            if (colNumber < 1 || colNumber > 9) { throw new Exception("Invalid column"); }

            int startPosition = (colNumber - 1) * sizeof(int);
            sbm.FS.Seek(startPosition, SeekOrigin.Begin);

            for (int i = 0; i < 9; i++)
            {
                colValue = sbm.BR.ReadInt32();
                sbm.FS.Seek(8 * sizeof(int), SeekOrigin.Current);

                col[i] = colValue;
            }

            return col;
        }

        public bool ColumnCorrect(int col)
        {
            bool correct = false;
            int[] checkCol = ReadColumn(col);

            if (Contains1To9(checkCol)) { correct = true; }

            return correct;
        }

        public int[] ReadSubMatrix(int row, int col)
        {
            int[] subMatrix = new int[9];
            int subMatrixValue;
            int countLimit = 0;

            if (row != 1 && row != 4 && row != 7) { throw new Exception("Invalid row"); }
            if (col != 1 && col != 4 && col != 7) { throw new Exception("Invalid column"); }

            int startPosition = ((row - 1) * 9 + (col - 1)) * sizeof(int); ;

            sbm.FS.Seek(startPosition, SeekOrigin.Begin);

            for (int i = 0; i < 9; i++)
            {
                if (countLimit == 3)
                {
                    sbm.FS.Seek(6 * sizeof(int), SeekOrigin.Current);
                    countLimit = 0;
                }
                subMatrixValue = sbm.BR.ReadInt32();
                subMatrix[i] = subMatrixValue;
                countLimit++;
            }

            return subMatrix;
        }
        public bool SubMatrixCorrect(int row, int col)
        {
            bool correct = false;
            int[] checkSubmatrix = ReadSubMatrix(row, col);

            if (Contains1To9(checkSubmatrix)) { correct = true; }

            return correct;
        }
        public bool SudokuCorrect()
        {
            bool correct = true;
            int i = 1;
            int j = 1;
            while (i < 9 && correct)
            {
                if (!RowCorrect(i) && !ColumnCorrect(i))
                {
                    correct = false;
                }

                if (i == 1 || i == 4 || i == 7)
                {
                    while (j < 7)
                    {
                        if (j == 1 || j == 4 || j == 7)
                        {
                            if (!SubMatrixCorrect(i, j))
                            {
                                correct = false;
                            }
                        }
                        j++;
                    }
                }
                i++;
            }
            return correct;
        }

        public void Stain(int row, int col, int value)
        {
            if (row < 1 && row > 9) { throw new Exception("Invalid row"); }
            if (col < 1 && col > 9) { throw new Exception("Invalid column"); }

            sbm.BR.Close();
            sbm.FS.Close();

            sbm.FS = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            BinaryWriter bW = new BinaryWriter(sbm.FS);

            int startPosition = (row * 9 + col) * sizeof(int); ;

            sbm.FS.Seek(startPosition, SeekOrigin.Begin);

            bW.Write(value);

            bW.Close();
            sbm.FS.Close();

            sbm.FS = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            sbm.BR = new BinaryReader(sbm.FS);
        }

        public void ShowMatrix(int[] matrix)
        {
            int countLimit = 0;

            for (int i = 0; i < matrix.Length; i++)
            {
                if (countLimit == 3)
                {
                    Console.WriteLine();
                    countLimit = 0;
                }
                Console.Write($"{matrix[i]} ");
                countLimit++;
            }
        }

        public void ShowArray(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                Console.Write($"{array[i]} ");
            }
        }
    }
}
