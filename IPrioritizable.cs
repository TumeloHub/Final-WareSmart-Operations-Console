using System;

namespace ConsoleApp1
{
    // Defines behaviour for objects that can have a priority.
    public interface IPrioritizable
    {
        double CalculatePriority();
        PriorityLevel GetPriorityLevel();
    }
}
