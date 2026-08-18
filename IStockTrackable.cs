using System;

namespace ConsoleApp1
{
    // Defines behaviour for objects that have stock which can be managed.
    public interface IStockTrackable
    {
        void AddStock(int amount);
        bool RemoveStock(int amount);
        bool NeedsRestocking();
    }
}
