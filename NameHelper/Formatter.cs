using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameHelper
{
    public class Formatter
    {
        private readonly IReadOnlyCollection<IFormatRule> _rules;
        private TextInfo _textInfo;

        public Formatter(IReadOnlyCollection<IFormatRule> rules)
        {
            _rules = rules ?? throw new ArgumentNullException(nameof(rules));
            _textInfo = CultureInfo.CurrentCulture.TextInfo;
        }

        private string _applyRules(string arg)
        {
            var result = arg;
            foreach (var rule in _rules)
            {
                result = rule.Apply(result);
            }

            return result;
        }

        public string Process(IEnumerable<string> args)
        {
            if (args == null) throw new ArgumentNullException(nameof(args));
            return string.Join(Environment.NewLine,
                args.Select(Format));
        }

        public string Format(string name)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            return _applyRules(_textInfo.ToTitleCase(name.ToLower()));
        }
    }
}