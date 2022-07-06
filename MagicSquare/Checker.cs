namespace MagicSquare.PropertyChecker;

public static class Checker
{
    private static int[,] _matrix;
    private static int _size;

    public static bool IsMagicSquare(int[,] matrix, int maxSize)
    {
        InitializePrivateFields(matrix, maxSize);

        int controlSum = GetMagicValue();

        return (CheckLines(controlSum) == true 
                && CheckColumns(controlSum) == true 
                &&CheckFirstDiag(controlSum) == true
                &&CheckSecondDiag(controlSum) == true);
    }

    private static void InitializePrivateFields(int[,] matrix, int maxSize)
    {
        _matrix = matrix;
        _size = maxSize;
    }

    public static int GetMagicValue()
    {
        return GetSumOnLine(0);
    }

    private static int GetSumOnLine(int lineNumber)
    {
        int sum = 0;
        for (int column = 0; column < _size; column++)
        {
            sum += _matrix[lineNumber, column];
        }

        return sum;
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
        int sum = 0;
        for (int line = 0; line < _size; line++)
        {
            sum += _matrix[line, column];
        }

        return sum;
    }

    private static bool CheckFirstDiag(int controlSum)
    {
        int sumOnFirstDiag = GetSumOnFirstDiag();

        return sumOnFirstDiag == controlSum;
    }

    private static int GetSumOnFirstDiag()
    {
        int sum = 0;
        for (int line = 0; line < _size; line++)
        {
            int column = line;

            sum += _matrix[line, column];
        }

        return sum;
    }

    private static bool CheckSecondDiag(int controlSum)
    {
        int sumOnSecondDiag = GetSumOnSecondDiag();

        return sumOnSecondDiag == controlSum;
    }

    private static int GetSumOnSecondDiag()
    {
        int sum = 0;
        for (int line = 0; line < _size; line++)
        {
            int column = _size-line-1;

            sum += _matrix[line, column];
        }

        return sum;
    }
}