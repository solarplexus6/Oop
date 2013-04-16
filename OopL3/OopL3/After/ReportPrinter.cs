using System;

namespace OopL3.After
{
    public class ReportPrinter
    {
        #region Private fields

        private readonly IDataFetcher _dataFetcher;
        private readonly IDocumentFormatter _documentFormatter;

        #endregion
        #region Ctors

        public ReportPrinter(IDataFetcher dataFetcher, IDocumentFormatter documentFormatter)
        {
            _dataFetcher = dataFetcher;
            _documentFormatter = documentFormatter;
        }

        #endregion
        #region Public methods

        public void PrintReport()
        {
            string data = _dataFetcher.GetData();
            string document = _documentFormatter.FormatDocument(data);
            Console.WriteLine(document);
        }

        #endregion
    }

    internal class DataFetcher : IDataFetcher
    {
        #region IDataFetcher Members

        public string GetData()
        {
            return "data";
        }

        #endregion
    }

    internal class DocumentFormatter : IDocumentFormatter
    {
        #region IDocumentFormatter Members

        public string FormatDocument(string document)
        {
            if (string.IsNullOrWhiteSpace(document))
            {
                return document;
            }
            const string REPORT_FORMAT = "== {0} == formatted";
            return String.Format(REPORT_FORMAT, document);
        }

        #endregion
    }

    public interface IDocumentFormatter
    {
        #region Public methods

        string FormatDocument(string document);

        #endregion
    }
}

public interface IDataFetcher
{
    #region Public methods

    string GetData();

    #endregion
}