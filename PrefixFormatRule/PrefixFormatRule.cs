using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using NameHelper;

namespace PrefixFormatRule
{
    public class PrefixFormatRule : IFormatRule, IConfigInitializer
    {
        public PrefixFormatRule()
        {
        }

        public PrefixFormatRule(string prefix)
        {
            _prefix = prefix;
        }

        private string _prefix = "";

        public string Apply(string arg)
        {
            var words = arg.Split(' ');
            for (int i = 0; i < words.Length; i++)
            {
                var word = words[i];
                if (word.IndexOf(_prefix, StringComparison.CurrentCultureIgnoreCase) == 0)
                {
                    var mainPart = string.Empty;
                    if (word.Length > _prefix.Length)
                    {
                        mainPart = char.ToUpper(word[_prefix.Length]) + word.Substring(_prefix.Length + 1);
                    }

                    words[i] = _prefix + mainPart;
                }
            }

            return string.Join(" ", words);
        }

        public void Init(XElement xConfig)
        {
            _prefix = xConfig.Element("Prefix")?.Value;
        }
    }
}