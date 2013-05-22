using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace Zad2
{
    public class XmlDataAccessHandler : DataAccessHandler
    {
        private readonly string _filePath;
        private XmlReader _reader;
        private List<string> _elementsNames;

        public string Result { get; private set; }

        public XmlDataAccessHandler(string filePath)
        {
            _filePath = filePath;
        }

        protected override void CloseConnection()
        {
            _reader.Dispose();
            _reader = null;
        }

        protected override void GetData()
        {
            _elementsNames = new List<string>();
            while (_reader.Read())
            {                
                if (_reader.NodeType == XmlNodeType.Element)
                {
                    _elementsNames.Add(_reader.Name);
                }
            }
        }

        protected override void OpenDataConnection()
        {
            _reader = XmlReader.Create(_filePath);
        }

        protected override void ProcessData()
        {
            _elementsNames.Sort((s1, s2) => -Comparer.Default.Compare(s1.Length, s2.Length));
            Result = _elementsNames.FirstOrDefault();
        }
    }
}