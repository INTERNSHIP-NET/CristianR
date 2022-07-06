
using ExtensionMethods;

namespace MagicSquare.Reader
{
    public static class TestReader
    {
        private const int MAX_LINE_SIZE = 100;
        private const int MAX_COLUMN_SIZE = 100;

        private const string SOLUTION_NAME = "MagicSquare";

        private static string _testFilePath;
        private static int _testNumber;

        public static int[,] GetMatrixForTest(int testNumber, out int maxSize)
        {
            InitializePrivateFields(testNumber);

            string matrixAsString = GetMatrixAsString();

            return GetMatrix(matrixAsString, out maxSize);
        }

        private static void InitializePrivateFields(int testNumber)
        {
            _testNumber = testNumber;

            _testFilePath = GetTestFilePath();
        }

        private static int[,] GetMatrix(string matrixAsString, out int maxSize)
        {
            /*
             * initializations
             */
            maxSize = 0;

            int[,] matrix = new int[MAX_LINE_SIZE, MAX_COLUMN_SIZE];
            int linesCounter = 0;
            int columnsCounter = 0;

            bool rowEndedInNewLine = false;
            int k = 0;
            while (k < matrixAsString.Length)
            {
                try
                {
                    int number = matrixAsString.GetNumberAtPosition(k);
                    matrix[linesCounter, columnsCounter] = number;

                    columnsCounter++;
                    k += GetDigitCounterOf(number);
                    rowEndedInNewLine = false;

                    if (columnsCounter > maxSize)
                    {
                        maxSize = columnsCounter;
                    }
                }
                catch (Exception e)
                {
                    if (matrixAsString[k] == '\n')
                    {
                        linesCounter++;
                        columnsCounter = 0;
                        rowEndedInNewLine = true;
                    }

                    k++;
                }
            }

            if (!rowEndedInNewLine) //meaning that linesCounter didn't update following the idea that more data has to come
            {
                linesCounter++;
            }

            if (linesCounter != maxSize)
            {
                throw new Exception("The input in the file doesn't follow the rule of: SQUARE_MATRIX");
            }
            else
            {
                return matrix;
            }
        }

        private static int GetDigitCounterOf(int number)
        {
            if (number == 0)
            {
                return 1;
            }

            int digitCounter = 0;
            while (number != 0)
            {
                digitCounter++;
                number /= 10;
            }

            return digitCounter;
        }

        private static string GetTestFilePath()
        {
            string pathBeforeExeFile = Directory.GetCurrentDirectory();

            int lengthOfSolutionName = SOLUTION_NAME.Length;
            int indexOfSolutionName = pathBeforeExeFile.LastIndexOf(SOLUTION_NAME);

            string pathToTest;
            pathToTest = pathBeforeExeFile.Substring(0, indexOfSolutionName + lengthOfSolutionName);

            string testNumberToString = _testNumber.ToString();
            pathToTest += "\\tests\\M" + testNumberToString + ".txt";

            /*
             * Note that the test folder called "tests" must be in the last file named as SOLUTION_NAME,
             * usually in solution folder
             */

            return pathToTest;
        }

        private static string GetMatrixAsString()
        {
            string matrixAsString;

            FileStream fileStream = null;
            StreamReader streamReader = null;

            try
            {
                FileInfo fileInfo = new FileInfo(_testFilePath);
                fileStream = fileInfo.Open(FileMode.Open, FileAccess.Read);

                streamReader = new StreamReader(fileStream);

                matrixAsString = streamReader.ReadToEnd();
            }
            catch (Exception e)
            {
                throw new Exception("Could not perform test for test_number: " + _testNumber.ToString() + '\n' +
                                    e.Message);
            }
            finally
            {
                if (streamReader != null)
                {
                    streamReader.Close();
                }

                if (fileStream != null)
                {
                    fileStream.Close();
                }
            }

            return matrixAsString;
        }
    }
}