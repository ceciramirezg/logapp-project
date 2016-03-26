using System;

namespace LogApplication
{
    [Flags]
    public enum MessageType
    {
        Message = 1,
        Warning = 2,
        Error = 4
    }
}
