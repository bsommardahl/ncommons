using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging.Configuration;

namespace NCommons.Logging.EnterpriseLibrary.Specs
{
    class ConfigurationSourceStub : IConfigurationSource
    {
        const string LoggingSettingsKey = "loggingConfiguration";
        readonly Dictionary<string, ConfigurationSection> _cache = new Dictionary<string, ConfigurationSection>();
        readonly object _sync = new object();

        public ConfigurationSection GetSection(string sectionName)
        {
            if (sectionName.Equals(LoggingSettingsKey))
                return GetLoggingSettings();

            return null;
        }

        public void Add(IConfigurationParameter saveParameter, string sectionName,
                        ConfigurationSection configurationSection)
        {
                
        }

        public void Remove(IConfigurationParameter removeParameter, string sectionName)
        {
                
        }

        public void AddSectionChangeHandler(string sectionName, ConfigurationChangedEventHandler handler)
        {
                
        }

        public void RemoveSectionChangeHandler(string sectionName, ConfigurationChangedEventHandler handler)
        {
                
        }

        ConfigurationSection GetLoggingSettings()
        {
            LoggingSettings loggingConfiguration = null;

            if (!_cache.ContainsKey(LoggingSettingsKey))
            {
                lock (_sync)
                {
                    if (!_cache.ContainsKey(LoggingSettingsKey))
                    {
                        loggingConfiguration = new LoggingSettings();

                        loggingConfiguration.Name = "Logging Application Block";
                        loggingConfiguration.TracingEnabled = true;
                        loggingConfiguration.DefaultCategory = "General";
                        loggingConfiguration.LogWarningWhenNoCategoriesMatch = true;

                        var customTraceListenerData = new CustomTraceListenerData
                                                          {
                                                              ListenerDataType =
                                                                  Type.GetType(
                                                                  "Microsoft.Practices.EnterpriseLibrary.Logging.Configuration.CustomTraceListenerData, Microsoft.Practices.EnterpriseLibrary.Logging, Version=4.1.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"),
                                                              TraceOutputOptions = TraceOptions.None,
                                                              Filter = SourceLevels.All,
                                                              Type =
                                                                  Type.GetType(
                                                                  "NCommons.Logging.EnterpriseLibrary.Specs.TraceListenerSpy, NCommons.Logging.EnterpriseLibrary.Specs"),
                                                              Name = "Custom Trace Listener",
                                                              Formatter = "Text Formatter"
                                                          };

                        loggingConfiguration.TraceListeners.Add(customTraceListenerData);

                        var textFormatterData = new TextFormatterData
                                                    {
                                                        Name = "Text Formatter",
                                                        Type =
                                                            Type.GetType(
                                                            "Microsoft.Practices.EnterpriseLibrary.Logging.Formatters.TextFormatter, Microsoft.Practices.EnterpriseLibrary.Logging, Version=4.1.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"),
                                                        Template = "{message}"
                                                    };

                        loggingConfiguration.Formatters.Add(textFormatterData);

                        loggingConfiguration.SpecialTraceSources.AllEventsTraceSource.TraceListeners.Add(
                            new TraceListenerReferenceData("Custom Trace Listener"));
                        loggingConfiguration.SpecialTraceSources.AllEventsTraceSource.DefaultLevel =
                            SourceLevels.All;
                        loggingConfiguration.SpecialTraceSources.AllEventsTraceSource.Name = "All Events";

                        _cache[LoggingSettingsKey] = loggingConfiguration;
                    }
                }
            }

            return _cache[LoggingSettingsKey];
        }
    }
}