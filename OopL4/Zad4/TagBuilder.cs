using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zad4
{
    public class TagBuilder
    {
        #region Private fields

        private Dictionary<string, string> _attributes = new Dictionary<string, string>();
        private StringBuilder _body = new StringBuilder();
        private TagBuilder _parent;
        private string _tagName;
        private int _level = -1;

        public bool IsIntended { get; set; }
        public int Indentation = 4;

        #endregion
        #region Ctors

        public TagBuilder()
        {           
        }

        public TagBuilder(string TagName, TagBuilder Parent)
        {
            _tagName = TagName;
            _parent = Parent;
            _level = Parent._level + 1;
            IsIntended = Parent.IsIntended;
            Indentation = Parent.Indentation;
        }

        #endregion
        #region Overrides

        public override string ToString()
        {
            var tag = new StringBuilder();

            // preamble
            if (!string.IsNullOrEmpty(_tagName))
            {
                tag.AppendFormat("{0}<{1}", IsIntended ? string.Empty.PadLeft(_level * Indentation) : String.Empty, _tagName);
            }

            if (_attributes.Count > 0)
            {
                tag.Append(" ");
                tag.Append(
                    string.Join(" ",
                                _attributes
                                    .Select(
                                        kvp => string.Format("{0}='{1}'", kvp.Key, kvp.Value))
                                    .ToArray()));
            }

            // body/ending
            if (_body.Length > 0)
            {
                if (!string.IsNullOrEmpty(_tagName) || _attributes.Count > 0)
                {
                    tag.Append(">");
                }
                if (IsIntended)
                {
                    _body.Append(Environment.NewLine);
                    _body.Append(string.Empty.PadLeft((_level + 1) * Indentation));
                }
                tag.Append(_body);
                if (IsIntended)
                {
                    _body.Append(Environment.NewLine);
                }
                if (!string.IsNullOrEmpty(_tagName))
                {
                    tag.AppendFormat("</{0}>", _tagName);
                }
            }
            else if (!string.IsNullOrEmpty(_tagName))
            {
                tag.Append("/>");
            }            
            return tag.ToString();
        }

        #endregion
        #region Public methods

        public TagBuilder AddAttribute(string Name, string Value)
        {
            _attributes.Add(Name, Value);
            return this;
        }

        public TagBuilder AddContent(string Content)
        {
            if (IsIntended)
            {
                _body.Append(Environment.NewLine);
                //_body.Append(string.Empty.PadLeft(_level * Indentation));
            }
            _body.Append(Content);
            return this;
        }

        public TagBuilder AddContentFormat(string Format, params object[] args)
        {            
            _body.AppendFormat(Format, args);
            return this;
        }

        public TagBuilder EndTag()
        {
            _parent.AddContent(ToString());
            return _parent;
        }

        public TagBuilder StartTag(string TagName)
        {            
            var tag = new TagBuilder(TagName, this);

            return tag;
        }

        #endregion
    }
}