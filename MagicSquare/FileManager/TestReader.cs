
using MagicSquare.FileManager.StringExtensions;

namespace MagicSquare.FileManager
{
    public static class TestReader
    {
        private const string SOLUTION_NAME = "MagicSquare";
        private const string RELATIVE_TEST_DIRECTORY = "\\tests\\"; //relative path from SOLUTION FOLDER beginning and ending with '\'
        public static string[] GetTestFilePaths()
        {
            string pathBeforeExeFile = Directory.GetCurrentDirectory();

            int lengthOfSolutionName = SOLUTION_NAME.Length;
            int indexOfSolutionName = pathBeforeExeFile.LastIndexOf(SOLUTION_NAME, StringComparison.Ordinal);

            string pathToTestFolder = pathBeforeExeFile.Substring(0, indexOfSolutionName + lengthOfSolutionName);
            pathToTestFolder += RELATIVE_TEST_DIRECTORY;
            try
            {
                return Directory.GetFiles(pathToTestFolder);
            }
            catch(Exception e)
            {
                throw new Exception("[FAILED] - UNABLE TO OPEN THE DIRECTORY \n " +
                                    "The tests couldn't be obtained because the directory specified doesn't exist or" +
                                    "you may have not enough permissions to open it.\n" 
                                    + e.Message);
            }
           
            /*
             * Note that the TEST_DIRECTORY in this case is in the last directory named as SOLUTION_NAME,
             * usually in solution directory
             */
        }
        public static List<List<int>> GetMatrixAsIntegers(string testPath, out int maxSize)
        {
            string matrixAsString = GetMatrixAsString(testPath);

            try
            {
                return GetMatrix(matrixAsString, out maxSize);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message + '\n' + "[FILE]: " + testPath + '\n');
            }
        }

        private static List<List<int>> GetMatrix(string matrixAsString, out int matrixSize)
        {
            /*
            * Initializations
            */

            List<List<int>> matrix = new List<List<int>>();

            const int COUNTER_BEGINNING = 0;
            matrixSize = COUNTER_BEGINNING;
            bool rowEndedInNewLine = false;

            /*
            * End of Initializations
            */

            int currentCharIndex = 0;
            List<int> currentRow = new List<int>();

            while (currentCharIndex < matrixAsString.Length)
            {
                try
                {
                    int cellNumber = matrixAsString.GetNumberAtPosition(currentCharIndex);
                    currentRow.Add(cellNumber);

                    currentCharIndex += GetDigitCounterOf(cellNumber);
                    rowEndedInNewLine = false;

                    if (currentRow.Count > matrixSize)
                    {
                        matrixSize = currentRow.Count;
                    }
                }
                catch (Exception)
                {
                    if (matrixAsString[currentCharIndex] == '\n')
                    {
                        matrix.Add(currentRow);
                        currentRow = new List<int>();

                        rowEndedInNewLine = true;
                    }

                    currentCharIndex++;
                }
            }

            if (!rowEndedInNewLine) //meaning that linesCounter didn't update following the idea that more data has to come
            {
                matrix.Add(currentRow);
            }

            if (matrix.Count != matrixSize)
            {
                throw new Exception("[FAILED] - UNABLE TO EXTRACT THE MATRIX \n" +
                                    "The input in the file doesn't follow the rule of: SQUARE_MATRIX");
            }

            return matrix;
        }

        private static int GetDigitCounterOf(int number)
        {
            int digitCounter = 0;
            
            if (number == 0)
            {
                digitCounter = 1;
            }
            else while (number != 0)
            {
                digitCounter++;
                number /= 10;
            }

            return digitCounter;
        }

        private static string GetMatrixAsString(string testPath)
        {
            string matrixAsString;

            FileStream? fileStream = null;
            StreamReader? streamReader = null;

            try
            {
                FileInfo fileInfo = new FileInfo(testPath);
                fileStream = fileInfo.Open(FileMode.Open, FileAccess.Read);

                streamReader = new StreamReader(fileStream);

                matrixAsString = streamReader.ReadToEnd();
            }
            catch (Exception e)
            {
                throw new Exception("[FAILED] - Could not perform test for the file:\n" + testPath + '\n' + e.Message);
            }
            finally
            {
                streamReader?.Close();

                fileStream?.Close();
            }

            return matrixAsString;
        }
    }
}