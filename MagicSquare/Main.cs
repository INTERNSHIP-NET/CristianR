// See https://aka.ms/new-console-template for more information

using System.Globalization;
using MagicSquare.PropertyChecker;
using MagicSquare.FileManager;

namespace MagicSquare
{
    public static class MagicSquare
    {
        private const char DIRECTORY_SEPARATOR = '\\';

        public static void Main(string[] args)
        {
            string[]? filePaths;

            try
            {
                filePaths = TestReader.GetTestFilePaths();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }


            foreach (var filePath in filePaths)
            {
                Console.WriteLine(); //New line to clear see the output more clear
                try
                {
                    List<List<int>> testMatrix = TestReader.GetMatrixAsIntegers(filePath, out int matrixSize);
                    
                    int lastIndexOfDirectorySeparator = filePath.LastIndexOf(DIRECTORY_SEPARATOR);
                    int firstIndexOfTestFile = lastIndexOfDirectorySeparator + 1;
                    string testFileName = filePath.Substring(firstIndexOfTestFile);
                    
                    Console.WriteLine("[TEST]: " + testFileName);
                    if (Checker.IsMagicSquare(testMatrix, matrixSize))
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
                    Console.WriteLine(e.Message);
                }
            }
        }

    }
}