namespace ExtensionMethods;

public static class StringExtensions 
{
    public static int GetNumberAtPosition(this string matrix, int startPosition)
    {
        if(IsNumber(matrix[startPosition]))
        {
            int number = 0;
            int i = startPosition;
            while (i < matrix.Length && IsNumber(matrix[i]))
            {
                int digit = (int)(matrix[i] - '0');
                number = number * 10 + digit;
                i++;
            }

            return number;
        }
        else
        {
            throw new Exception("Can't extract a number because starting position doesn't match a number.");
        }
    }
    
    private static bool IsNumber(char c)
    {
        if (c >= '0' && c <= '9')
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}