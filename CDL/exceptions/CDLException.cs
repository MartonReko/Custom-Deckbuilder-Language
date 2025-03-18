namespace CDL.exceptions;

public class CDLException(int line, int column, string message)
{
    public int line = line;
    public int column = column;
    public string message = message;

    public override string ToString()
    {
        return $"Line {line}, column {column}: {message}" ;
    }
}