using A3.DAO;

internal class Program
{
    private static void Main(string[] args)
    {
        IDAO sbm = SBMFactory.CreateSBMDAO("SUDOKU_OK.TXT");
        IDAO sbm1 = SBMFactory.CreateSBMDAO("SUDOKU_KO.TXT");

        for (int i = 0; i < sbm.ReadRow(4).Length; i++)
        {
            Console.Write(sbm.ReadRow(6)[i] + " ");
        }
        Console.WriteLine("\n");

        for (int i = 0; i < sbm.ReadColumn(6).Length; i++)
        {
            Console.WriteLine(sbm.ReadColumn(6)[i] + " ");
        }
        Console.WriteLine("\n");

        int[] subMatrix = sbm.ReadSubMatrix(7, 4);
        sbm.ShowMatrix(subMatrix);

        Console.WriteLine("\n");

        Console.WriteLine("\nSUDOKU OK\n-------------");
        if (sbm.SudokuCorrect()) { Console.WriteLine("SUDOKU CORRECT"); }
        else { Console.WriteLine("SUDOKU WRONG"); }

        sbm.Stain(4, 4, 5);
        if (sbm.SudokuCorrect()) { Console.WriteLine("SUDOKU CORRECT"); }
        else { Console.WriteLine("SUDOKU WRONG"); }

        sbm.Stain(4, 4, 8);
        if (sbm.SudokuCorrect()) { Console.WriteLine("SUDOKU CORRECT"); }
        else { Console.WriteLine("SUDOKU WRONG"); }

        Console.WriteLine("\nSUDOKU KO\n-------------");
        if (sbm1.SudokuCorrect()) { Console.WriteLine("SUDOKU CORRECT"); }
        else { Console.WriteLine("SUDOKU WRONG"); }
    }
}
