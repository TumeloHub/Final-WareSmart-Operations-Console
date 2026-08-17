using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class StockInfo : EventArgs
    {
        public Product Product { get; }
        public int OldStock { get; }
        public int NewStock { get; }

        public StockInfo(Product product, int oldStock, int newStock)
        {
            Product = product;
            OldStock = oldStock;
            NewStock = newStock;
        }
    }
}
