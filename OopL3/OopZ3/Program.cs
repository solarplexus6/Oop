using System;
using System.Collections;
using System.Collections.Generic;

namespace OopZ3
{
    class Program
    {
        static void Main(string[] args)
        {
            var items = new[]
                {
                    new After.ConcreteItem {Name = "one", Price = (decimal) 3.5, Category = "fun"},
                    new After.ConcreteItem {Name = "two", Price = (decimal) 10.1, Category = "notfun"},
                    new After.ConcreteItem {Name = "three", Price = (decimal) 1.1, Category = "chłam"} 
                };

            var register = new After.CashRegister(new After.TaxCalculator());
            register.PrintBill(items);
            
            Console.WriteLine();

            var comparer =
                Comparer<After.ConcreteItem>.Create((ix, iy) => Comparer.Default.Compare(ix.Category, iy.Category));
            register.PrintBill(items, comparer);
        }
    }
}
