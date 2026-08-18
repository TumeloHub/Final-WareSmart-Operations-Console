using System;

namespace ConsoleApp1
{
    // Exception used when warehouse save or load operations fail.
    public class WarehousePersistenceException : Exception
    {
        public WarehousePersistenceException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
