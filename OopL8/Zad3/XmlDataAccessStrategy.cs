using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace Zad3
{
    public class XmlDataAccessStrategy : IDataAccessStrategy
    {
        #region Private fields

        private readonly string _filePath;

        #endregion
        #region Ctors

        public XmlDataAccessStrategy(string filePath)
        {
            _filePath = filePath;
        }

        #endregion
        #region IDataAccessStrategy Members

        public int GetResultFromData()
        {
            using (var reader = XmlReader.Create(_filePath))
            {
                var elementsNames = new List<string>();
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        elementsNames.Add(reader.Name);
                    }
                }

                elementsNames.Sort((s1, s2) => -Comparer.Default.Compare(s1.Length, s2.Length));
                var firstOrDefault = elementsNames.FirstOrDefault();
                if (firstOrDefault != null)
                {
                    return firstOrDefault.Length;
                }
            }

            return 0;
        }

        #endregion
    }
}