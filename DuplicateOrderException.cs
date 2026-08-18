using System;

namespace ConsoleApp1
{
    // Exception used when an order with the same ID already exists.
    public class DuplicateOrderException : Exception
    {
        public DuplicateOrderException(string message) : base(message)
        {
        }
    }
}
