using System.Collections.Generic;

namespace NameHelper
{
    public class Helper
    {
        private static Formatter _formatter;

        public static string FormatName(string name)
        {
            if (_formatter == null)
            {
                var rules = new FormatRulesLoader().Load();
                _formatter = new Formatter(rules);

            }
            return _formatter.Format(name);
        }
    }
}