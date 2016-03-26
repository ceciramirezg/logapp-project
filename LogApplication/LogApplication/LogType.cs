using System;

namespace LogApplication
{
    [Flags]
    public enum LogType
    {
        TextFile = 1,
        Console = 2,
        Database = 4
    }
}
