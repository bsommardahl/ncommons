using System;
using System.Collections.Generic;

namespace NCommons.Rules
{
    public interface ICommandProcessor
    {
        IEnumerable<ReturnValue> Process(object message, IMissingCommandStrategy missingCommandStrategy);
    }
}