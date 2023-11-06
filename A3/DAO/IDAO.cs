using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A3.DAO
{
    public interface IDAO
    {
        bool Contains1To9(int[] data);
        int[] ReadRow(int rowNumber);
        bool RowCorrect(int row);
        int[] ReadColumn(int colNumber);
        bool ColumnCorrect(int col);
        int[] ReadSubMatrix(int row, int col);
        bool SubMatrixCorrect(int row, int col);
        bool SudokuCorrect();
        void Stain(int row, int col, int value);
        void ShowMatrix(int[] matrix);
        void ShowArray(int[] array);
    }
}
