namespace MagicSquare.FileManager.StringExtensions;

public static class StringExtensions 
{
    public static int GetNumberAtPosition(this string matrix, int startPosition)
    {
        if(IsNumber(matrix[startPosition]))
        {
            const int ZERO_ASCII_CODE = '0';
            const int ZERO_CONCATENATION = 10;
            
            int cellNumberAsInteger = 0;
            int searchingIndex = startPosition;
            
            while (searchingIndex < matrix.Length && IsNumber(matrix[searchingIndex]))
            {
                int digitAsciiCode = matrix[searchingIndex];
                int digitAsInteger = (digitAsciiCode - ZERO_ASCII_CODE);
                
                cellNumberAsInteger = cellNumberAsInteger * ZERO_CONCATENATION + digitAsInteger;
                searchingIndex++;
            }

            return cellNumberAsInteger;
        }
        else
        {
            throw new Exception("Can't extract a number because starting position doesn't match a number.");
        }
    }
    
    private static bool IsNumber(char c)
    {
        if (c is >= '0' and <= '9')
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}