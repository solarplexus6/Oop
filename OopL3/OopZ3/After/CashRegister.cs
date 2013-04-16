using System;
using System.Collections.Generic;
using System.Linq;

namespace OopZ3.After
{
    public class TaxCalculator : ITaxCalculator
    {
        #region Public methods

        public Decimal CalculateTax(Decimal price)
        {
            return price*(decimal) 0.22;
        }

        #endregion
    }

    public interface ITaxCalculator
    {
        #region Public methods

        Decimal CalculateTax(Decimal price);

        #endregion
    }

    public interface IItem
    {
        #region Properties

          string Name { get; set; }
          Decimal Price { get; set; }

        #endregion
    }

    public class ConcreteItem : IItem
    {
        public  string Name { get; set; }
        public  decimal Price { get; set; }

        public string Category { get; set; }
    }

    public class CashRegister
    {
        #region Private fields

        private readonly ITaxCalculator _taxCalc;

        #endregion
        #region Ctors

        public CashRegister(ITaxCalculator taxCalc)
        {
            _taxCalc = taxCalc;
        }

        #endregion
        #region Public methods

        public Decimal CalculatePrice(IEnumerable<IItem> items)
        {
            return items.Sum(item => item.Price + _taxCalc.CalculateTax(item.Price));
        }

        public void PrintBill<T>(IEnumerable<T> items, IComparer<T> comparer = null) where T : IItem
        {
            var orderedItems = comparer == null
                                              ? items.AsEnumerable()
                                              : items.OrderBy(item => item, comparer);
            foreach (T item in orderedItems)
            {
                Console.WriteLine("towar {0} : cena {1} + podatek {2}",
                                  item.Name, item.Price, _taxCalc.CalculateTax(item.Price));
            }
        }

        #endregion


    }
}