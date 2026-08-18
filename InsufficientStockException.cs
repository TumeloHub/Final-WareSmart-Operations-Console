using System;

namespace ConsoleApp1
{
    // Exception used when more stock is removed than is available.
    public class InsufficientStockException : Exception
    {
        public InsufficientStockException(string message) : base(message)
        {
        }
    }
}
