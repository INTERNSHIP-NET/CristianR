// See https://aka.ms/new-console-template for more information

using MagicSquare.PropertyChecker;
using MagicSquare.Reader;

namespace MagicSquare
{
    class MagicSquare
    {
        public static void Main(string[] args)
        {
            for (int k = 1; k <= 10; k++)
            {
                try
                {
                    int maxSize;

                    int[,] matrix = TestReader.GetMatrixForTest(k, out maxSize);

                    Console.WriteLine("[SUCCESS]");

                    if (Checker.IsMagicSquare(matrix, maxSize) == true)
                    {
                        Console.WriteLine("It is a MAGIC SQUARE. The magic value is: " + Checker.GetMagicValue());
                    }
                    else
                    {
                        Console.WriteLine("It's NOT a MAGIC SQUARE. Can't calculate magic value");
                    }
                    
                }
                catch (Exception e)
                {
                    Console.WriteLine("[FAILED]");
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}