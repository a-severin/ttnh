using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace NameHelper
{
    public class FormatRulesLoader
    {
        private IEnumerable<XElement> _loadRuleConfiguration()
        {
            var assemblyLocation = GetType().Assembly.Location;
            var folder = Path.GetDirectoryName(assemblyLocation);
            var configPath = Path.Combine(folder, "RuleConfig.xml");
            if (!File.Exists(configPath))
            {
                return Enumerable.Empty<XElement>();
            }

            var xConfig = XDocument.Load(configPath);
            return xConfig.Descendants("Rule");
        }

        private IEnumerable<Type> _loadRuleTypes()
        {
            var assemblyLocation = GetType().Assembly.Location;
            var folder = Path.GetDirectoryName(assemblyLocation);
            var ruleTypes = new List<Type>();
            foreach (var path in Directory.GetFiles(folder, "*FormatRule.dll"))
            {
                var assembly = Assembly.LoadFrom(path);
                foreach (var ruleType in assembly.GetTypes()
                    .Where(_ => _.GetInterface(typeof(IFormatRule).FullName) != null))
                    ruleTypes.Add(ruleType);
            }

            return ruleTypes;
        }

        public IReadOnlyCollection<IFormatRule> Load()
        {
            var rules = new List<IFormatRule>();

            var ruleTypes = _loadRuleTypes();

            var mapTypeToRule = ruleTypes.ToDictionary(_ => _.Name, _ => _);

            var xConfigs = _loadRuleConfiguration();
            foreach (var xConfig in xConfigs)
            {
                var typeConfig = xConfig.Attribute("Type")?.Value;
                if (typeConfig == null || !mapTypeToRule.TryGetValue(typeConfig, out var ruleType)) continue;

                var rule = Activator.CreateInstance(ruleType);
                if (rule is IConfigInitializer initializer) initializer.Init(xConfig);
                rules.Add(rule as IFormatRule);
            }

            return rules;
        }
    }
}