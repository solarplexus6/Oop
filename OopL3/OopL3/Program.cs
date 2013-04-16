using System;

namespace OopL3
{
    internal class Program
    {
        #region Private methods

        private static void Main(string[] args)
        {
            Console.WriteLine("Before:");
            var beforePrinter = new Before.ReportPrinter();
            beforePrinter.PrintReport();

            Console.WriteLine("After:");
            var afterPrinter = new After.ReportPrinter(new After.DataFetcher(), new After.DocumentFormatter());
            afterPrinter.PrintReport();
        }

        #endregion
    }
}