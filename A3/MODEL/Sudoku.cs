using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A3.MODEL
{
    public class Sudoku
    {
        private FileStream fS;
        private BinaryReader bR;
        private string fileName;

        public Sudoku(string fileName)
        {
            FileName = fileName;
            FS = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            BR = new BinaryReader(FS);
        }

        public FileStream FS { get => fS; set => fS = value; }
        public BinaryReader BR { get => bR; set => bR = value; }
        public string FileName { get => fileName; set => fileName = value; }       
    }
}
