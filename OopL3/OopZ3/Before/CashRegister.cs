using System;
using System.Linq;

namespace OopZ3.Before
{
    public class TaxCalculator
    {
        #region Public methods

        public Decimal CalculateTax(Decimal price)
        {
            return price*(decimal) 0.17;
        }

        #endregion
    }

    public class Item
    {
        #region Properties

        public string Name { get; set; }
        public Decimal Price { get; set; }

        #endregion
    }

    public class CashRegister
    {
        #region Private fields

        public TaxCalculator taxCalc = new TaxCalculator();

        #endregion
        #region Public methods

        public Decimal CalculatePrice(Item[] items)
        {
            return items.Sum(item => item.Price + taxCalc.CalculateTax(item.Price));
        }

        public void PrintBill(Item[] items)
        {
            foreach (Item item in items)
            {
                Console.WriteLine("towar {0} : cena {1} + podatek {2}",
                                  item.Name, item.Price, taxCalc.CalculateTax(item.Price));
            }
        }

        #endregion
    }
}