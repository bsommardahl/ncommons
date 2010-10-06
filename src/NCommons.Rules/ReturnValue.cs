using System;

namespace NCommons.Rules
{
    public class ReturnValue
    {
        public static ReturnValue Empty = new ReturnValue();

        public Type Type { get; set; }

        public object Value { get; set; }

        public ReturnValue SetValue<T>(T input)
        {
            Type = typeof (T);
            Value = input;
            return this;
        }
    }
}