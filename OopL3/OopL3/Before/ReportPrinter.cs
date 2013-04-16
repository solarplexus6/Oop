using System;

namespace OopL3.Before
{
    public class ReportPrinter
    {
        #region Private fields

        private string _data;

        #endregion
        #region Public methods

        public void FormatDocument()
        {
            if (string.IsNullOrWhiteSpace(_data))
            {
                return;
            }
            const string REPORT_FORMAT = "== {0} == formatted";
            _data = String.Format(REPORT_FORMAT, _data);
        }

        public string GetData()
        {
            return "data";
        }

        public void PrintReport()
        {
            _data = GetData();
            FormatDocument();
            Console.WriteLine(_data);
        }

        #endregion
    }
}