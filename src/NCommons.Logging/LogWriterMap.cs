using System.Collections.Generic;

namespace NCommons.Logging
{
    /// <summary>
    /// Provides an implementation of <see cref="ILogWriter"/> which provides a mapping strategy
    /// for varying persistance based upon the value of an abitrary property of the <see cref="LogEntry"/>.
    /// </summary>
    /// <typeparam name="T">The type to be used as the map key</typeparam>
    public abstract class LogWriterMap<T> : ILogWriter
    {
        protected IDictionary<T, LogWriterProvider> Map { get; set; }

        public virtual void Write(LogEntry logEntry)
        {
            T propertyValue = GetPropertyValue(logEntry);

            if (Map.ContainsKey(propertyValue))
                Map[propertyValue](logEntry);
        }

        protected abstract T GetPropertyValue(LogEntry logEntry);
    }

    public static class DictionaryExtensions
    {
        public static object ValueOrDefault(this IDictionary<string, object> dictionary, string key)
        {
            object value = null;

            if (dictionary.ContainsKey(key))
                value = dictionary[key];

            return value;
        }
    }
}