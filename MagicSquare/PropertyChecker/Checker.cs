namespace MagicSquare.PropertyChecker;

public static class Checker
{
    private const int FIRST_LINE_OF_THE_MATRIX = 0; 
    private static List<List<int>>? _matrix;
    private static int _size;

    public static bool IsMagicSquare(List<List<int>>matrix, int maxSize)
    {
        InitializePrivateFields(matrix, maxSize);

        int controlSum = GetSumOnLine(FIRST_LINE_OF_THE_MATRIX);

        return (CheckLines(controlSum)
                && CheckColumns(controlSum)
                && CheckFirstDiag(controlSum)
                && CheckSecondDiag(controlSum));
    }

    private static void InitializePrivateFields(List<List<int>> matrix, int matrixSize)
    {
        _matrix = matrix;
        _size = matrixSize;
    }

    public static int GetMagicValue()
    {
        return GetSumOnLine(FIRST_LINE_OF_THE_MATRIX);
    }

    private static int GetSumOnLine(int lineNumber)
    {
        if (_matrix != null)
        {
            int sum = 0;
            for (int column = 0; column < _size; column++)
            {
                sum += _matrix[lineNumber][column];
            }
            return sum;
        }
        else
        {
            throw new Exception("[FAILED] - Can't access matrix fields \n" +
                                        "Matrix is null");
        }
    }

    private static bool CheckLines(int controlSum)
    {
        for (int line = 0; line < _size; line++)
        {
            int sumOnLine = GetSumOnLine(line);

            if (sumOnLine != controlSum)
            {
                return false;
            }
        }

        return true;
    }

    private static bool CheckColumns(int controlSum)
    {
        for (int column = 0; column < _size; column++)
        {
            int sumOnColumn = GetSumOnColumn(column);
            
            if (sumOnColumn != controlSum)
            {
                return false;
            }
        }

        return true;
    }

    private static int GetSumOnColumn(int column)
    {
        if (_matrix != null)
        {
            int sum = 0;
            for (int line = 0; line < _size; line++)
            {
                sum += _matrix[line][column];
            }

            return sum;
        }
        else
        {
            throw new Exception("[FAILED] - Can't access matrix fields \n" +
                                "Matrix is null");
        }
    }

    private static bool CheckFirstDiag(int controlSum)
    {
        int sumOnFirstDiag = GetSumOnFirstDiag();

        return sumOnFirstDiag == controlSum;
    }
    private static int GetSumOnFirstDiag()
    {
        if (_matrix != null)
        {
            int sum = 0;
            for (int line = 0; line < _size; line++)
            {
                int column = line;

                sum += _matrix[line][column];
            }

            return sum;
        }
        else
        {
            throw new Exception("[FAILED] - Can't access matrix fields \n" +
                                "Matrix is null");
        }
    }

    private static bool CheckSecondDiag(int controlSum)
    {
        int sumOnSecondDiag = GetSumOnSecondDiag();

        return sumOnSecondDiag == controlSum;
    }

    private static int GetSumOnSecondDiag()
    {
        if (_matrix != null)
        {
            int sum = 0;
            for (int line = 0; line < _size; line++)
            {
                int column = _size - line - 1;

                sum += _matrix[line][column];
            }

            return sum;
        }
        else
        {
            throw new Exception("[FAILED] - Can't access matrix fields \n" +
                                "Matrix is null");
        }
    }
}